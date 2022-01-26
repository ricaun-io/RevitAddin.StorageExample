using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitAddin.StorageExample.Revit.Commands;
using ricaun.Revit.UI;
using System;

namespace RevitAddin.StorageExample.Revit
{
    [Console]
    public class App : IExternalApplication
    {
        private static RibbonPanel ribbonPanel;
        public Result OnStartup(UIControlledApplication application)
        {
            ribbonPanel = application.CreatePanel("StorageExample");
            ribbonPanel.AddStackedItems(
                ribbonPanel.NewPushButtonData<CommandSave>("Save ProjectInfo"),
                ribbonPanel.NewPushButtonData<CommandLoad>("Load ProjectInfo"),
                ribbonPanel.NewPushButtonData<CommandReset>("Reset ProjectInfo")
            );

            ribbonPanel.AddSeparator();

            ribbonPanel.AddStackedItems(
                ribbonPanel.NewPushButtonData<CommandWallSave>("Save Walls"),
                ribbonPanel.NewPushButtonData<CommandWallLoad>("Load Walls"),
                ribbonPanel.NewPushButtonData<CommandWallSelect>("Select Walls")
            );

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            ribbonPanel.Close();
            return Result.Succeeded;
        }
    }
}