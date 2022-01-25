using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitAddin.StorageExample.Services;
using System;

namespace RevitAddin.StorageExample.Revit
{
    [Transaction(TransactionMode.Manual)]
    public class CommandSave : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            UIApplication uiapp = commandData.Application;

            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document document = uidoc.Document;

            StorageTextService storageService = new StorageTextService();
            var projectInfo = document.ProjectInformation;

            using (Transaction transaction = new Transaction(document))
            {
                transaction.Start("CommandSave");
                storageService.Save(projectInfo, $"Teste {DateTime.Now}");
                transaction.Commit();
            }

            TaskDialog.Show("Revit", storageService.Load(projectInfo));

            return Result.Succeeded;
        }
    }

}