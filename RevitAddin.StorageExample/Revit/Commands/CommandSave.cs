using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitAddin.StorageExample.Services;
using RevitAddin.StorageExample.Views;
using System;

namespace RevitAddin.StorageExample.Revit.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class CommandSave : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            UIApplication uiapp = commandData.Application;

            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document document = uidoc.Document;

            var storageService = new StorageProjectInfoService();

            InputView inputView = new InputView("Save some Storage on ProjectInfo!");
            if (inputView.ShowDialog() == false) return Result.Succeeded;

            using (Transaction transaction = new Transaction(document))
            {
                transaction.Start("Save");
                storageService.Save(document, inputView.Text);
                transaction.Commit();
            }

            TaskDialog.Show("Revit", storageService.Load(document));

            return Result.Succeeded;
        }
    }

}