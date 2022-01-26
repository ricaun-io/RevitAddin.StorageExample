using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitAddin.StorageExample.Services;
using System.Linq;

namespace RevitAddin.StorageExample.Revit.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class CommandWallSelect : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            UIApplication uiapp = commandData.Application;

            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document document = uidoc.Document;

            StorageWallService storageService = new StorageWallService();

            var walls = storageService.Select(document);

            TaskDialog.Show("Revit", $"Select {walls.Count()} with Storage");

            return Result.Succeeded;
        }
    }

}