# sms emitter
Huge number of applications need to send SMS to different phone numbers in their business logic. Thus, this is essential to have an independent service for handling delivery mechanisms of SMS messages to their corresponding phone numbers, and make sure that all message will eventually be sent even if massive numbers of messages are waiting for sending. SMS emitter is an independent, self-hosted service which can be used as windows service in windows server. SMS emitter is decoupled from other applications and any application can use it for sending asynchronous SMS message to specific numbers using Message Queuing (MSMQ). Even with massive messages traffic, SMS emitter guarantee that all messages will be delivered finally. For this Purpose, SMS emitter uses Message Queuing (MSMQ) for Enqueue large number of Message each time and dequeue message for sending them to numbers.
 ## Getting Started
These instructions are will get you to how to setting up and how to install SMS Emitter on your local computer and servers, how to use it as windows service, and how to use SMS Emitter inside applications and send SMS messages to it from applications. 
### Prerequisite 
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
### Installing Message Queueing (MSMQ) on windows 10
For installing MSMQ on windows 10 you should firstly turn on Microsoft Message Queue (MSMQ) Server by using following instructions:

```
Control Panel => Programs => Programs and Features => Turn Windows features on or off 
```
And then enable “Microsoft Message Queueing (MSMQ) Server”.
After Running SMS Emitter make sure that, SMS Emitter have permission to access all Queues. By going in following path 
```
Comptuer Management => Services and Applications => Message Queueing => Private Queues
```
In this location you can see all SMS Emitter Queues which is prioritized base on Normal, Medium, High and Very High. Make sure that all queues have appropriate permission by clicking on Queues and going to security tab.
### Installing as windows service 
SMS Emitter can be runned directly by executing exe file inside Libs folder after building project or installing as windows service inside windows servers. Best option for using SMS Emitter is installing it as windows service. For these purpose, open command prompt as administrator and do flowing instructions:
Firstly, build solution, then go to following path
```
SmsService.Api => Libs
```
Then use following command in command prompt and press enter:
```
C:\Windows\Microsoft.NET\Framework64\v4.0.30319\InstallUtil.exe SmsService.Api.exe
```
Now SMS Service is installed as windows service and is ready to use.
