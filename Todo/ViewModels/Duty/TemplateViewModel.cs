using Newtonsoft.Json;
using Prism.Dialogs;
using System.Collections.ObjectModel;
using Todo.Common.Dialogs;
using Todo.DragDrop.Models;
using Todo.IService;

namespace Todo.ViewModels.Duty
{
    public class TemplateViewModel : BindableBase, IDialogAware
    {
        private readonly IDialogHostService dialogService;
        private readonly IDutyTemplateService templateService;
        public TemplateViewModel(IDialogHostService dialogHostServiceArg,IDutyTemplateService templateServiceArg)
        {
            templateService=templateServiceArg;
            ToolItems = new ObservableCollection<ShapeBase>()
               {
                   new RectangleBaseToolItem(){Width=100,Height=40},
                     new RectangleBaseToolItem(){Width=100,Height=40},
                       new RectangleBaseToolItem(){Width=100,Height=40}
               };
            MouseDownCommand = new DelegateCommand(CanvasMouseDown);
            GetColorCommand = new DelegateCommand<string>(OpenColorPicker);
            ExecuteCommand = new DelegateCommand<string>(Excute);
            dialogService = dialogHostServiceArg;
        }

        private void Excute(string type)
        {
            if (type == "Save")
            {
                templateService.SaveTemplate(new Entity.DutyTemplate()
                {
                    Content = JsonConvert.SerializeObject(Items)
                });
            }
        }

        private async void OpenColorPicker(string colorType)
        {
            var parameters = new DialogParameters();
            parameters.Add("Type", colorType);
             var dialogResult = await dialogService.ShowDialog("ColorPickerView", parameters, "Template");

            if (dialogResult.Result is ButtonResult.OK)
            {
                if (dialogResult.Parameters["Type"]?.ToString() == "FillColor")
                {
                    this.SelectedItem.FillColor = dialogResult.Parameters["Value"]?.ToString() ?? "";
                }
                if (dialogResult.Parameters["Type"]?.ToString() == "FontColor")
                {
                    this.SelectedItem.FontColor = dialogResult.Parameters["Value"]?.ToString() ?? "";
                }
            }
            
        }

        private void CanvasMouseDown()
        {
             
        }

        public DialogCloseListener RequestClose { get; set; }


        #region Props
        private ObservableCollection<ShapeBase> _toolItems = new ObservableCollection<ShapeBase>();
        public ObservableCollection<ShapeBase> ToolItems
        {
            get { return _toolItems; }
            set { SetProperty(ref _toolItems, value); }
        }

        private ShapeBase _selectedToolItem = new ShapeBase();
        public ShapeBase SelectedToolItem
        {
            get { return _selectedToolItem; }
            set { SetProperty(ref _selectedToolItem, value); }
        }


        private ShapeBase _selectedItem =new ShapeBase();
        public ShapeBase SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }

        private ObservableCollection<ShapeBase> _items = new ObservableCollection<ShapeBase>();
        public ObservableCollection<ShapeBase> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }

        public DelegateCommand MouseDownCommand { get; set; }

        public DelegateCommand<string> GetColorCommand { get; set; }

        public DelegateCommand<string> ExecuteCommand { get; set; }

        #endregion
        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {

        }
    }
}
