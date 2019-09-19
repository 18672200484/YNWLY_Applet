 
/*汽车衡监控界面*/
 
 var TruckWeighterV8Cef;
    if (!TruckWeighterV8Cef)TruckWeighterV8Cef = {};

    (function() {
        // 道闸1升杆
      TruckWeighterV8Cef.Gate1Up = function() {
        native function Gate1Up();
        Gate1Up();
      };   
      
        // 道闸1降杆
      TruckWeighterV8Cef.Gate1Down = function() {
        native function Gate1Down();
        Gate1Down();
      };   
      
        // 道闸2升杆
      TruckWeighterV8Cef.Gate2Up = function() {
        native function Gate2Up();
        Gate2Up();
      };   
      
        // 道闸2降杆
      TruckWeighterV8Cef.Gate2Down = function() {
        native function Gate2Down();
        Gate2Down();
      };  
      
    })(); 