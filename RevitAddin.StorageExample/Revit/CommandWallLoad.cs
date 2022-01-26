using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitAddin.StorageExample.Services;

namespace RevitAddin.StorageExample.Revit
{
    [Transaction(TransactionMode.Manual)]
    public class CommandWallLoad : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            UIApplication uiapp = commandData.Application;

            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document document = uidoc.Document;

            StorageWallService storageService = new StorageWallService();

            var walls = storageService.Select(document);
            var text = "";
            foreach (var wall in walls)
            {
                var data = storageService.Load(wall);
                text += $"{wall.Id} - {data}\n";
            }
            TaskDialog.Show("Revit", text);

            return Result.Succeeded;
        }
    }

}