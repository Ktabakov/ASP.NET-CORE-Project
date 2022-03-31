﻿using CryptoTradingPlatform.Core.Contracts;
using CryptoTradingPlatform.Data.Models;
using CryptoTradingPlatform.Infrastructure.Data;
using CryptoTradingPlatform.Core.Models.Trading;
using CryptoTradingPlatfrom.Core.Contracts;
using CryptoTradingPlatfrom.Core.Models.Assets;
using CryptoTradingPlatfrom.Core.Models.Trading;
using Microsoft.EntityFrameworkCore;
using CryptoTradingPlatfrom.Core.Models.Api;
using Microsoft.AspNetCore.Mvc;

namespace CryptoTradingPlatfrom.Core.Services
{
    public class TradingService : ITradingService
    {
        private readonly ApplicationDbContext data;
        private readonly ICryptoApiService cryptoService;
        public TradingService(ApplicationDbContext _data, ICryptoApiService _cryptoService)
        {
            data = _data;
            cryptoService = _cryptoService;
        }

        public async Task<bool> SaveSwap (BuyAssetFormModel model, string userName)
        {
            var user = await data.Users.FirstOrDefaultAsync(c => c.UserName == userName);
            var sellAsset = await data.Assets.FirstOrDefaultAsync(c => c.Id == model.SellAssetId);
            var buyAsset = await data.Assets.FirstOrDefaultAsync(c => c.Id == model.BuyAssetId);
            var sellQuantity = model.SellAssetQyantity;
            var buyQuantity = model.BuyAssetQuantity;
            bool success = false;

            var buyCryptoResponseModel = await cryptoService.GetCryptos(new List<string> { buyAsset.Ticker });
            var buyAssetPrice = buyCryptoResponseModel.FirstOrDefault().Price;

            if (buyAsset == null || sellAsset == null)
            {
                return false;
            }

            var sellUserAsset = await data
                .UserAssets
                .Where(c => c.ApplicationUserId == user.Id)
                .FirstOrDefaultAsync(c => c.AssetId == model.SellAssetId);

            var buyUserAsset = await data
                .UserAssets
                .Where(c => c.ApplicationUserId == user.Id)
                .FirstOrDefaultAsync(c => c.AssetId == model.BuyAssetId);

            if (sellUserAsset == null || buyUserAsset == null)
            {
                return false;
            }

            var sellAssetUserQuantity = data
                .UserAssets
                .Where(c => c.ApplicationUserId == user.Id)
                .FirstOrDefault(c => c.AssetId == model.SellAssetId)
                .Quantity;

            if (sellAssetUserQuantity - sellQuantity < 0)
            {
                return false;
            }
            decimal transactionFee = (Convert.ToDecimal(buyQuantity) * buyAssetPrice) * 0.01M;
            if (user.Money - transactionFee < 0)
            {
                return false;
            }

            user.Money -= transactionFee;

            Transaction transaction = new Transaction()
            {
                ApplicationUserId = user.Id,
                AssetId = buyAsset.Id,
                Date = DateTime.Now,
                Price = buyAssetPrice,
                Quantity = buyQuantity,
                Type = "Swap",
                TransactionFee = transactionFee,
            };

            try
            {
                sellUserAsset.Quantity -= sellQuantity;
                buyUserAsset.Quantity += buyQuantity;
                await data.Transactions.AddAsync(transaction);
                data.Treasury.FirstOrDefault().Total += transactionFee;
                await data.SaveChangesAsync();
                success = true;
                
            }
            catch (Exception)
            {
                return false;
            }
            return success;
        }
        public async Task<bool> SaveTransaction(TradingFormModel model, string userName)
        {
            var user = await data.Users.FirstOrDefaultAsync(c => c.UserName == userName);
            var asset = await data.Assets.FirstOrDefaultAsync(c => c.Name == model.Name);
            bool success = false;
            decimal quantityToDouble = model.Quantity; 

            if (asset == null)
            {
                return false;
            }

            decimal sum = model.Price * model.Quantity;

            if (user.Money < sum && model.Type == "Buy")
            {
                return false;
            }
            decimal transactionFee = 0;

            try
            {
                var userAsset = await data
                    .UserAssets
                    .Where(c => c.ApplicationUserId == user.Id)
                    .FirstOrDefaultAsync(c => c.AssetId == asset.Id);

                if (model.Type == "Buy")
                {
                    if (userAsset == null)
                    {
                        data.UserAssets
                            .Add(new UserAsset()
                            {
                                ApplicationUserId = user.Id,
                                AssetId = asset.Id,
                                Quantity = quantityToDouble
                            });
                    }
                    else
                    {
                        userAsset.Quantity += quantityToDouble;
                    }
                    user.Money -= sum;
                }
                else if (model.Type == "Sell")
                {
                    if (userAsset == null)
                    {
                        return false;
                    }
                    else
                    {
                        if (userAsset.Quantity - quantityToDouble < 0)
                        {
                            return false;
                        }
                        userAsset.Quantity -= quantityToDouble;
                    }
                    if (userAsset.Quantity == 0)
                    {
                        try
                        {
                            data.UserAssets.Remove(userAsset);
                        }
                        catch (Exception)
                        {

                            return false;
                        }
                    }
                    user.Money += sum;
                }
                if (user.Money - transactionFee < 0)
                {                  
                    return false;
                }
                transactionFee = sum * 0.01M;
                user.Money -= transactionFee;
            }
            catch (Exception)
            {
                return false;
            }

            

            Transaction transaction = new Transaction()
            {
                ApplicationUser = user,
                Asset = asset,
                Date = DateTime.Now,
                Price = model.Price,
                Quantity = quantityToDouble,
                Type = model.Type,
                TransactionFee = transactionFee
            };
            try
            {
                await data.Transactions.AddAsync(transaction);
                data.Treasury.FirstOrDefault().Total += transactionFee;              
                await data.SaveChangesAsync();
                success = true;
            }
            catch (Exception)
            {
                return false;
            }

            return success;
        }

        public async Task<bool> SaveToFavorites(string ticker, string? userName)
        {
            var userId = data.Users.FirstOrDefault(c => c.UserName == userName).Id;
            var assetId = data.Assets.FirstOrDefault(c => c.Ticker == ticker).Id;
            bool success = false;

            if (String.IsNullOrEmpty(userId) || String.IsNullOrEmpty(assetId))
            {
                return success;
            }
            var userFavoriteEntry = await data.UserFavorites.FirstOrDefaultAsync(c => c.ApplicationUserId == userId && c.AssetId == assetId);

            try
            {
                if (userFavoriteEntry == null)
                {
                    await data.UserFavorites.AddAsync(new UserFovorites { ApplicationUserId = userId, AssetId = assetId });
                }
                else
                {
                    data.UserFavorites.Remove(userFavoriteEntry);
                }
                await data.SaveChangesAsync();
                success = true;
            }
            catch (Exception)
            {
                return success;
            }

            return success;
        }

        public async Task<List<TransactionHistoryViewModel>> GetUserTradingHistory(string? name)
        {
            return await data
                .Transactions
                .Where(c => c.ApplicationUser.UserName == name)
                .Include(c => c.Asset)
                .Select(c => new TransactionHistoryViewModel
                {
                    AssetName = c.Asset.Name,
                    Date = c.Date,
                    Price = c.Price,
                    Quantity = c.Quantity,
                    Type = c.Type,
                    Fee = c.TransactionFee
                })
               .OrderByDescending(c => c.Date)
               .ToListAsync();
        }
        public async Task<decimal> CalculateTransaction(BuyAssetFormModel model)
        {
            string sellAssetTicker = data
                .Assets
                .FirstOrDefault(a => a.Id == model.SellAssetId)
                .Ticker;

            string buyAssetTicker = data
                .Assets
                .FirstOrDefault(a => a.Id == model.BuyAssetId)
                .Ticker;

            List<string> tickers = new List<string> { buyAssetTicker, sellAssetTicker };
            BuyAssetResponseModel responseModel = await cryptoService.GetPrices(tickers);

            decimal sellAssetPriceUSD = model.SellAssetQyantity * responseModel.SellAssetPrice;
            decimal buyAssetQuantity = sellAssetPriceUSD / responseModel.BuyAssetPrice;

            return buyAssetQuantity;
        }

     
        public List<TransactionHistoryViewModel> SortTransactions(string sortOrder, List<TransactionHistoryViewModel> transactions)
        {

            switch (sortOrder)
            {
                case "Name":
                    transactions = transactions.OrderBy(s => s.AssetName).ToList();
                    break;
                case "name_desc":
                    transactions = transactions.OrderByDescending(s => s.AssetName).ToList();
                    break;
                case "Date":
                    transactions = transactions.OrderBy(s => s.Date).ToList();
                    break;
                case "date_desc":
                    transactions = transactions.OrderByDescending(s => s.Date).ToList();
                    break;
                case "Type":
                    transactions = transactions.OrderBy(s => s.Type).ToList();
                    break;
                case "type_desc":
                    transactions = transactions.OrderByDescending(s => s.Type).ToList();
                    break;
                case "Quantity":
                    transactions = transactions.OrderBy(s => s.Quantity).ToList();
                    break;
                case "quantity_desc":
                    transactions = transactions.OrderByDescending(s => s.Quantity).ToList();
                    break;
                case "Price":
                    transactions = transactions.OrderBy(s => s.Price).ToList();
                    break;
                case "price_desc":
                    transactions = transactions.OrderByDescending(s => s.Price).ToList();
                    break;
                case "Fee":
                    transactions = transactions.OrderBy(s => s.Fee).ToList();
                    break;
                case "fee_desc":
                    transactions = transactions.OrderByDescending(s => s.Fee).ToList();
                    break;
                default:
                    transactions = transactions.OrderByDescending(s => s.Date).ToList();
                    break;
            }
            return transactions;
        }
    }
    
}
