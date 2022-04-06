# Crypto Trading Platform
:mortar_board: This project was built as the defense project for ASP.NET Core course at Software University. Many thanks [Stamo Petkov](https://github.com/stamo) and the whole [SoftUni Team](https://softuni.bg/)!

# :memo: Overview
Demo project for trading cryptocurrencies. Once registered, you directly get 300.000USD to test the platform with. On each transaction there is a 0.01% Transaction Fee, which is then added to the platform's treasury.
As a user of the plaftorm you can see live crypto prices, live news update (both thanks to the Coinmarketcap API and CrtyoPanic API respectively), buy and sell cryptocurrencies, as well as swap them for one another. You can add cryptos to your favorites list
and see them in a seperate page. You can also see your transaction history, sort it, and download it.
In the admin area the administrators can look at statistics about the trades on the platform, see a list of all platform's users where they can promote them or demote them.
On a seperate page admins can see all users applications to become managers and take actions. Admins can delete the application or approve it, promoting the user directly into a manager on the platform.
Managers, once logged in, see few more options on their menu screen than the users. Managers are given the important task to add articles for the users
to help them along their trading journey. Managers can also add new assets to the platform. As this is a demo project, the simplest way to add a crypto asset to the platform
is to write down the asset's ticker(BTC, ETH, ADA, BNB). The rest is handeled by the backend with the Coinmarketcap API.

&nbsp;&nbsp;&nbsp;&nbsp;**Test Accounts**:

&nbsp;&nbsp;&nbsp;&nbsp;Username: admin@abv.bg  
&nbsp;&nbsp;&nbsp;&nbsp;Password: admin123  

&nbsp;&nbsp;&nbsp;&nbsp;Username: manager@abv.bg  
&nbsp;&nbsp;&nbsp;&nbsp;Password: manager123  

# 🛠 Built with:
* ASP.NET Core 6.0 MVC
* ASP.NET Core areas
* MSSQL Server
* Entity Framwork Core 6.0.2
* [Coinmarketcap API](https://coinmarketcap.com/api/)
* [CryptoPanic API](https://cryptopanic.com/developers/api/)
* Bootstrap
* Newtonsoft.Json
* NUnit
* Sqlite
* AJAX 
* JavaScript
* jQuery
* HTML 5
* CSS
* toastr

## :wrench: **Database**
[Microsoft SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) and [Entity Framework Core](https://dotnet.microsoft.com/download)  

[![DB.jpg](https://i.postimg.cc/sXTKBn2s/DB.jpg)](https://postimg.cc/N9rRSkTz)
## :framed_picture: Screenshots - Home Page
[![Home1.jpg](https://i.postimg.cc/rmPKbWmq/Home1.jpg)](https://postimg.cc/34280kmq)
[![Home2.jpg](https://i.postimg.cc/Kj3RyzkB/Home2.jpg)](https://postimg.cc/GTrcFbjm)
[![Home3.jpg](https://i.postimg.cc/wvv10F9W/Home3.jpg)](https://postimg.cc/KK6cYPkL)

## :framed_picture: Screenshots - Trade
[![Trade.jpg](https://i.postimg.cc/J4J5N76s/Trade.jpg)](https://postimg.cc/nCFmZxtx)

## :framed_picture: Screenshots - Swap
[![Swap.jpg](https://i.postimg.cc/zDyjfFTS/Swap.jpg)](https://postimg.cc/dDKdHdHL)

## :framed_picture: Screenshots - Transaction History
[![Transactions.jpg](https://i.postimg.cc/G36jTTQ0/Transactions.jpg)](https://postimg.cc/hhbmHj6M)

## :framed_picture: Screenshots - Details
[![Crypto-Details.jpg](https://i.postimg.cc/k5wVQLP6/Crypto-Details.jpg)](https://postimg.cc/f3tTZ2LD)
[![Articles-Details.jpg](https://i.postimg.cc/yN7gSstC/Articles-Details.jpg)](https://postimg.cc/MnrKC2z9)

## :framed_picture: Screenshots - Manager Application
[![Manager-Aplication.jpg](https://i.postimg.cc/fRcSLJHv/Manager-Aplication.jpg)](https://postimg.cc/cvJLk4GK)

## :framed_picture: Screenshots - Manager Job
[![Add-Article.jpg](https://i.postimg.cc/SNFY66zh/Add-Article.jpg)](https://postimg.cc/8s4CDrHK)
[![AddAsset.jpg](https://i.postimg.cc/02YMczpJ/AddAsset.jpg)](https://postimg.cc/hfGtGPZS)

## :framed_picture: Screenshots - Admin Area
[![All-Application.jpg](https://i.postimg.cc/mkK1cMfm/All-Application.jpg)](https://postimg.cc/ZvF52WqN)
[![AllUsers.jpg](https://i.postimg.cc/9Xk7N3Ry/AllUsers.jpg)](https://postimg.cc/Yj10hsxS)
[![Statistics.jpg](https://i.postimg.cc/14x4RQwT/Statistics.jpg)](https://postimg.cc/QKbjSv6k)
