 
/*集中管控首页*/

 var HomeYNWLYV8Cef;
    if (!HomeYNWLYV8Cef)HomeYNWLYV8Cef = {};
    
    (function() {
        // 打开皮带采样机监控界面
      HomeYNWLYV8Cef.OpenTrainBeltSampler = function() {
        native function OpenTrainBeltSampler();
        OpenTrainBeltSampler();
      }; 
        
      // 打开火车机械采样机监控界面
      HomeYNWLYV8Cef.OpenTrainMachinerySampler = function() {
        native function OpenTrainMachinerySampler();
        OpenTrainMachinerySampler();
      };  

        // 打开全自动制样机监控界面
      HomeYNWLYV8Cef.OpenAutoMaker = function() {
        native function OpenAutoMaker();
        OpenAutoMaker();
      };  

        // 打开火车入厂翻车机监控
      HomeYNWLYV8Cef.OpenTrainTipper = function() {
        native function OpenTrainTipper();
        OpenTrainTipper();
      };  

        // 打开火车入厂记录查询
      HomeYNWLYV8Cef.OpenWeightBridgeLoadToday = function() {
        native function OpenWeightBridgeLoadToday();
        OpenWeightBridgeLoadToday();
      };  
      
        // 打开汽车入厂重车衡监控
      HomeYNWLYV8Cef.OpenTruckWeighter = function() {
        native function OpenTruckWeighter();
        OpenTruckWeighter();
      };  

        // 打开汽车入厂重车衡监控
      HomeYNWLYV8Cef.OpenTruckOrder = function() {
        native function OpenTruckOrder();
        OpenTruckOrder();
      };  

        // 打开汽车机械采样机监控
      HomeYNWLYV8Cef.OpenTruckMachinerySampler = function() {
        native function OpenTruckMachinerySampler();
        OpenTruckMachinerySampler();
      };   

        // 打开智能存样柜与气动传输监控
      HomeYNWLYV8Cef.OpenAutoCupboard = function() {
        native function OpenAutoCupboard();
        OpenAutoCupboard();
      };  

        // 打开化验室监控
      HomeYNWLYV8Cef.OpenLaboratory = function() {
        native function OpenLaboratory();
        OpenLaboratory();
      };    

//        // 打开化验室监控
//      HomeYNWLYV8Cef.OpenLaboratory = function() {
//        native function OpenLaboratory();
//        getStorageInfo();
//      };    

      //查看仓位信息
      HomeYNWLYV8Cef.getStorageInfo=function(paramSampler){
      native function getStorageInfo(paramSampler);
      return getStorageInfo(paramSampler);
      };
      
       //查看故障信息
      HomeYNWLYV8Cef.GetHitchs=function(paramSampler){
      native function GetHitchs(paramSampler);
      return GetHitchs(paramSampler);
      };

      
    })(); 