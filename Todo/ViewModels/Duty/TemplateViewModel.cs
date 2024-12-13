using DryIoc;
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
        private readonly string currentView = "Template";
        public TemplateViewModel(IDialogHostService dialogHostServiceArg,IDutyTemplateService templateServiceArg)
        {
            templateService=templateServiceArg;
            dialogService = dialogHostServiceArg;

            ToolItems = new ObservableCollection<ShapeBase>()
               {
                   new RectangleBaseToolItem(){Width=100,Height=40},
                     new RectangleBaseToolItem(){Width=100,Height=40},
                       new RectangleBaseToolItem(){Width=100,Height=40}
               };
            FieldItems = new ObservableCollection<FieldModel>()
            {
                new FieldModel(){FieldName="领导",FieldValue="Leader"},
                 new FieldModel(){FieldName="领导电话",FieldValue="LeaderTel"},
                  new FieldModel(){FieldName="值班人员",FieldValue="Dutyer"},
                   new FieldModel(){FieldName="值班人员电话",FieldValue="DutyerTel"}
            };
            MouseDownCommand = new DelegateCommand(CanvasMouseDown);
            GetColorCommand = new DelegateCommand<string>(OpenColorPicker);
            ExecuteCommand = new DelegateCommand<string>(Excute);
            SelectionChangeCommand = new DelegateCommand<object>(SelectionChange);


           InitData();
        }

        private void SelectionChange(object obj)
        {
            if (!Items.Any())
            {
                dialogService.ShowWarningDialog($"请选择合适的控件完成模版", currentView);
                return;
            }
            this.SelectedItem.FieldValue = SelectedField;
        }

        private void InitData()
        {
            var model = templateService.GetSingle(1);
            if (model != null)
            {
                var datas = JsonConvert.DeserializeObject<List<RectangleBase>>(model.Content);
                foreach (var data in datas)
                { 
                    Items.Add(data);
                }
            } 

        }

        public class FieldModel : BindableBase
        {
            private string _fieldName;
            public string FieldName
            {
                get { return _fieldName; }
                set { SetProperty(ref _fieldName, value); }
            }

            private string _fieldValue;
            public string FieldValue
            {
                get { return _fieldValue; }
                set { SetProperty(ref _fieldValue, value); }
            }


        }
        private void Excute(string type)
        {
            if (type == "Save")
            {
                if (Items.Any())
                {
                    templateService.SaveTemplate(new Entity.DutyTemplate()
                    {
                        Content = JsonConvert.SerializeObject(Items)
                    });
                    dialogService.ShowSuccessDialog(currentView);
                } 
               else
                {
                    dialogService.ShowWarningDialog($"请选择合适的控件完成模版",currentView);
                }
            }
        }

        /// <summary>
        /// 打开取色器
        /// </summary>
        /// <param name="colorType"></param>
        private async void OpenColorPicker(string colorType)
        {
            if (SelectedItem.Id is null)
            {
                await dialogService.ShowWarningDialog("请先选择控件!", currentView);
                return;
            }
            var parameters = new DialogParameters();
            parameters.Add("Type", colorType);
             var dialogResult = await dialogService.ShowDialog("ColorPickerView", parameters, currentView);

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


        private ObservableCollection<FieldModel> fieldItems = new ObservableCollection<FieldModel>();
        public ObservableCollection<FieldModel> FieldItems
        {
            get { return fieldItems; }
            set { SetProperty(ref fieldItems, value); }
        }
        private string _selectedField;
        public string SelectedField
        {
            get => _selectedField;
            set => SetProperty(ref _selectedField, value);
        } 
        public DelegateCommand MouseDownCommand { get; set; }

        public DelegateCommand<string> GetColorCommand { get; set; }

        public DelegateCommand<string> ExecuteCommand { get; set; }

        public DelegateCommand<object> SelectionChangeCommand { get; set; }

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
