# 通訊工具
## ChatTool.Domain: 通訊工具領域層
* 通訊工具共通參考項目
</br>

## ChatTool.Persistent: 通訊工具持久層
* 資料存放位置
</br>

## ChatTool.Persistent.Tests: 通訊工具持久層單元測試
* 資料存放邏輯單元測試
</br>

## ChatTool.Server: 通訊工具Server
### nuget引用套件 
* System.Runtime.Caching: .net運用記憶體的擴充套件
* Autofac: 控制反轉(IOC)跟依賴(DI)注入工具
* Autofac.WebApi2: autofac在Web api的擴充套件
* Microsoft.AspNet.SignalR.Core: 微軟的signalr套件
* Microsoft.AspNet.WebApi.Core: 微軟的web api套件
* Microsoft.Owin: 微軟owin中間件
### 程式架構
* ActionHandler: 長連結處理
* Applibs: 工具參考
* Controllers: 短連結處理
* Hubs: Signalr hub
* Model: 模組、物件
</br>

## ChatTool.Server.Tests: 通訊工具Server單元測試
* 通訊工具Server商業邏輯單元測試驗證

## ChatTool.UI: 通訊工具UI介面
### nuget引用套件 
* Microsoft.AspNet.SignalR.Client: 微軟signalr客戶端套件
### 程式架構
* ActionHandler: 長連結處理
* Applibs: 工具參考
* Forms: UI、表單
* Model: 模組、物件
* Signalr: Signalr client
</br>

## ChatTool.UI.Tests: 通訊工具UI介面單元測試
* 通訊工具UI介面商業邏輯單元測試