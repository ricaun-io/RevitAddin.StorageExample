using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitAddin.StorageExample.Services;
using System;
using System.Collections.Generic;

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

            var storageService = new StorageProjectInfoService();

            using (Transaction transaction = new Transaction(document))
            {
                transaction.Start("Save");
                storageService.Save(document, $"ProjectInfo {DateTime.Now}");
                transaction.Commit();
            }

            TaskDialog.Show("Revit", storageService.Load(document));

            return Result.Succeeded;
        }
    }

}