using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using RevitAddin.StorageExample.Services;
using System.Linq;

namespace RevitAddin.StorageExample.Revit.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class CommandWallReset : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            UIApplication uiapp = commandData.Application;

            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document document = uidoc.Document;
            Selection selection = uidoc.Selection;

            StorageWallService storageService = new StorageWallService();

            var elements = selection.GetElementIds().Select(id => document.GetElement(id));
            var walls = elements.Cast<Wall>();

            using (Transaction transaction = new Transaction(document))
            {
                transaction.Start("Reset");

                foreach (var wall in walls)
                {
                    storageService.Reset(wall);
                }

                transaction.Commit();
            }

            TaskDialog.Show("Revit", $"Reseted {walls.Count()}");

            return Result.Succeeded;
        }
    }

}