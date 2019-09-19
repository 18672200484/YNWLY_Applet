using System;
using System.Collections.Generic;
using System.Text;
//
using Xilium.CefGlue;
using Xilium.CefGlue.Wrapper;
using CMCS.Monitor.Win.CefGlue;

namespace Xilium.CefGlue.Demo
{
    public class MonitorRenderProcessHandler : CefRenderProcessHandler
    {
        protected override void OnWebKitInitialized()
        {
            // 注册CefTester脚本
            CefRuntime.RegisterExtension("CefTester.Register", System.IO.File.ReadAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Web\CefTester\Resources\js", "register.js")), new CefTesterCefV8Handler());

            // 注册集中管控脚本
            CefRuntime.RegisterExtension("HomePage.Register", System.IO.File.ReadAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Web\HomePage\Resources\js", "register.js")), new HomePageCefV8Handler());
            // 注册物流园集中管控脚本
            CefRuntime.RegisterExtension("HomeYNWLY.Register", System.IO.File.ReadAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Web\HomeYNWLY\Resources\js", "register.js")), new HomeYNWLYCefV8Handler());
            // 注册汽车衡脚本
            CefRuntime.RegisterExtension("TruckWeighter.Register", System.IO.File.ReadAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Web\TruckWeighter\Resources\js", "register.js")), new TruckWeighterCefV8Handler());
            // 注册汽车采样机脚本
            CefRuntime.RegisterExtension("CarSampler.Register", System.IO.File.ReadAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Web\CarSampler\Resources\js", "register.js")), new CarSamplerCefV8Handler());
            // 注册皮带采样机脚本
            CefRuntime.RegisterExtension("TrainBeltSampler.Register", System.IO.File.ReadAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Web\TrainBeltSampler\Resources\js", "register.js")), new TrainBeltSamplerCefV8Handler());
            // 注册汽车监控脚本
            CefRuntime.RegisterExtension("CarMonitor.Register", System.IO.File.ReadAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Web\CarMonitor\Resources\js", "register.js")), new CarMonitorCefV8Handler());
            // 注册全自动制样机脚本
            CefRuntime.RegisterExtension("AutoMaker.Register", System.IO.File.ReadAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Web\AutoMaker\Resources\js", "register.js")), new AutoMakerCefV8Handler());
            // 注册智能存样柜及气动传输脚本
            CefRuntime.RegisterExtension("AutoCupboardPneumaticTransfer.Register", System.IO.File.ReadAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Web\AutoCupboardPneumaticTransfer\Resources\js", "register.js")), new AutoCupboardCefV8Handler());
            // 注册智能存样柜及气动传输
            CefRuntime.RegisterExtension("AutoCupboard.Register", System.IO.File.ReadAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Web\AutoCupboardPneumaticTransfer\Resources\js", "register.js")), new AutoCupboardCefV8Handler());
            // 注册翻车机
            CefRuntime.RegisterExtension("TrainUpender.Register", System.IO.File.ReadAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Web\TrainUpender\Resources\js", "register.js")), new TrainUpenderCefV8Handler());

            base.OnWebKitInitialized();
        }

        protected override void OnContextCreated(CefBrowser browser, CefFrame frame, CefV8Context context)
        {
            CefV8Value cefV8Value = context.GetGlobal();
            cefV8Value.SetValue("appName", CefV8Value.CreateString("CMCS.Monitor.Win"), CefV8PropertyAttribute.ReadOnly);

            base.OnContextCreated(browser, frame, context);
        }

        protected override bool OnProcessMessageReceived(CefBrowser browser, CefProcessId sourceProcess, CefProcessMessage message)
        {
            return base.OnProcessMessageReceived(browser, sourceProcess, message);
        }
    }
}
