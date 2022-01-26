using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using RevitAddin.StorageExample.Services;
using RevitAddin.StorageExample.Views;
using System;
using System.Linq;

namespace RevitAddin.StorageExample.Revit.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class CommandWallSave : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            UIApplication uiapp = commandData.Application;

            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document document = uidoc.Document;
            Selection selection = uidoc.Selection;

            StorageWallService storageService = new StorageWallService();

            InputView inputView = new InputView("Save some Storage on Wall!");
            if (inputView.ShowDialog() == false) return Result.Succeeded;

            var elements = selection.GetElementIds().Select(id => document.GetElement(id));
            var walls = elements.Cast<Wall>();

            using (Transaction transaction = new Transaction(document))
            {
                transaction.Start("Save");

                foreach (var wall in walls)
                {
                    storageService.Save(wall, inputView.Text);
                }

                transaction.Commit();
            }

            TaskDialog.Show("Revit", $"Save {walls.Count()}");

            return Result.Succeeded;
        }
    }

}