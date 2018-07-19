# SMSEmitter
Huge number of applications need to send SMS to different phone numbers in their business logic. Thus, this is essential to have an independent service for handling delivery mechanisms of SMS messages to their corresponding phone numbers, and make sure that all message will eventually be sent even if massive numbers of messages are waiting for sending. SMS emitter is an independent, self-hosted service which can be used as windows service in windows server. SMS emitter is decoupled from other applications and any application can use it for sending asynchronous SMS message to specific numbers using Message Queuing (MSMQ). Even with massive messages traffic, SMS emitter guarantee that all messages will be delivered finally. For this Purpose, SMS emitter uses Message Queuing (MSMQ) for Enqueue large number of Message each time and dequeue message for sending them to numbers.
 ## Getting Started
Firstly, clone SMS Service Project on your local computer using following command 
```
git clone https://github.com/MalekiSirius/SmsEmitter.git  
```
Secondly, SMS Emitter required a database for its transactions processes, for initialize database you should execute scripts in following path:
```
SmsService.Infrastructure.DAL => DatabaseScripts
```
And execute following scripts in SSMS:
```
DB_Data_init.sql, DB_Init.sql
```
You Should execute all procedures in following path in SSMS:
```
SmsService.Infrastructure.DAL => DatabaseScripts => Procedures
```
Thirdly, you need to setting up your DataBase Connection String in Following path and setting Data Source, Catalog, User ID and Password:


```
SmsService.Api => App.config
```
And setting up following connection string:
```
<add name="SmsService" connectionString="Data Source=.;Initial Catalog=SmsService;User ID=xxx; Password=xxx" />
```
You should also, setting up connection string in following path in T4 template for generating model from database:
```
SmsService.Infrastructure.DAL => DatabaseModel.tt
```
And change following connection string:
```
<#
	string dataSource = ".",
		   database = "SMSService",
		   username = "sirius",
		   password = "1234567";

var generator = new DatabaseModel.Generator($"Data Source={dataSource};Initial Catalog={database};User ID={username}; Password={password}", "dbo");
#>
```
Congratulation, SMS emitter is ready to use. 

