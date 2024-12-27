using DryIoc;
using DryIoc.ImTools;
using Newtonsoft.Json;
using Prism.Dialogs;
using System.Collections.ObjectModel;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Todo.Base;
using Todo.Common.Dialogs;
using Todo.DragDrop.Models;
using Todo.IService;
using Todo.Service;

namespace Todo.ViewModels.Duty
{
    public class TemplateViewModel : BindableBase, IDialogAware
    {
        private readonly IDialogHostService dialogService;
        private readonly IDutyTemplateService templateService;
        private readonly IDynamicFieldService dynamicFieldService;
        private readonly string currentView = "Template";
        public TemplateViewModel(IDialogHostService dialogHostServiceArg, IDutyTemplateService templateServiceArg, IDynamicFieldService dynamicFieldServiceArg)
        {
            templateService = templateServiceArg;
            dialogService = dialogHostServiceArg;
            dynamicFieldService = dynamicFieldServiceArg;

            var fields = dynamicFieldService.GetDataLists();

            ToolItems = new ObservableCollection<ShapeBase>()
            {
                new RectangleBaseToolItem(){Width=100,Height=40,DisplayName="文本",DisplayColor="LightGray",BaseType=DefaultConst.BaseType_Label},
                 new RectangleBaseToolItem(){Width=100,Height=40,DisplayName="日期",DisplayColor="LightBlue",BaseType=DefaultConst.BaseType_DateTime},
            };
            FieldItems = new ObservableCollection<FieldModel>() { }; //绑定字段
            foreach (var fieldMol in fields)
            {
                ToolItems.Add(new RectangleBaseToolItem()
                {
                    Width = 100,
                    Height = 40,
                    DisplayName = fieldMol.Field_Ch??"",
                    FieldName= fieldMol.Field_Ch ?? "",
                    FieldValue=fieldMol.Field_En??"",
                    DisplayColor =fieldMol.Field_Color ?? ""
                });
                FieldItems.Add(new FieldModel()
                {
                    FieldName = fieldMol.Field_Ch ?? "",
                    FieldValue = fieldMol.Field_En ?? "",
                });
            }
            foreach (var selectMol in DefaultConst.ConstDtFormats.Split(';'))
            {
                DtFormats.Add(new FieldModel()
                {
                    FieldName = selectMol,
                    FieldValue = selectMol
                });
            }
             
            MouseDownCommand = new DelegateCommand(CanvasMouseDown);
            GetColorCommand = new DelegateCommand<string>(OpenColorPicker);
            ExecuteCommand = new DelegateCommand<string>(Excute);
            SelectionChangeCommand = new DelegateCommand<object>(SelectionChange);
            DtFormatsChangeCommand = new DelegateCommand<object>(DtFormatsChange);

            RadioButtonCommand = new DelegateCommand<object>(RadioButtonChange);
            RemoveBgCommand = new DelegateCommand<object>(RemoveBg);
            RemoveControlCommand = new DelegateCommand<object>(RemoveControl);
            IsShowForm = false;
            IsShowControl = true;
            VisForm = Visibility.Hidden;
            VisControl = Visibility.Visible;
            BtnRemoveVisiable = Visibility.Hidden;
            FormProp = new FormProp()
            {
                Width = 1920,
                Height = 1080,
                BgColor = ""
            };
            // MapImg= new BitmapImage(new Uri("../Images/111.png", UriKind.Relative));
            //InitData();
        }
        /// <summary>
        /// 控件删除
        /// </summary>
        /// <param name="obj"></param>
        /// <exception cref="NotImplementedException"></exception>
        private async void RemoveControl(object objId)
        {
            var dialogResult = await dialogService.ShowWarningDialog($"是否删除控件?", currentView);
            if (dialogResult.Result is ButtonResult.OK)
            {
                var removeMol = Items.FindFirst(s => s.Id == objId.ToString());
                if (removeMol != null)
                {
                    Items.Remove(removeMol);
                }
            }
        }

        private async void RemoveBg(object obj)
        {
            var dialogResult = await dialogService.ShowWarningDialog($"是否删除?", currentView);
            if (dialogResult.Result is ButtonResult.OK)
            {
                FormProp.BgImgName = "";
                FormProp.BgImgUrl = "";
                BtnRemoveVisiable = Visibility.Hidden;
                MapImg = null;
            }

        }

        private void RadioButtonChange(object obj)
        {
            switch (obj.ToString())
            {
                case "true":
                    IsShowForm = true;
                    IsShowControl = false;
                    VisForm = Visibility.Visible;
                    VisControl = Visibility.Hidden;
                    break;
                case "false":
                    IsShowForm = false;
                    IsShowControl = true;
                    VisForm = Visibility.Hidden;
                    VisControl = Visibility.Visible;
                    break;
            }

        }

        private void SelectionChange(object obj)
        {
            if (!Items.Any())
            {
                dialogService.ShowWarningDialog($"请选择合适的控件完成模版", currentView);
                return;
            }
            SelectedItem.FieldName = SelectedField;
        }

        private void DtFormatsChange(object obj)
        {
            if (!Items.Any())
            {
                dialogService.ShowWarningDialog($"请选择合适的控件完成模版", currentView);
                return;
            }
            SelectedItem.DtFormat = SelectedField;
            SelectedItem.BaseContent = DateTime.Now.ToString(SelectedField);
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
            //新增或保存
            if (type == "Save")
            {
                if (Items.Any())
                {
                    var model = templateService.GetSingle(1);
                    var tempModel = new TemplateObject()
                    {
                        TempFormProp = FormProp,
                        ShapeBases = Items.ToList()
                    };
                    if (model?.Id > 0)
                    {
                        model.Content = JsonConvert.SerializeObject(tempModel);
                        templateService.UpDateTemplate(model);

                    }
                    else
                    {

                        templateService.SaveTemplate(new Entity.DutyTemplate()
                        {
                            Content = JsonConvert.SerializeObject(tempModel)
                        });
                    }


                    dialogService.ShowSuccessDialog(currentView);
                }
                else
                {
                    dialogService.ShowWarningDialog($"请选择合适的控件完成模版", currentView);
                }
            }
        }

        /// <summary>
        /// 打开取色器
        /// </summary>
        /// <param name="colorType"></param>
        private async void OpenColorPicker(string colorType)
        {

            if (SelectedItem.Id is null && colorType != "BgColor")
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

                if (dialogResult.Parameters["Type"]?.ToString() == "BgColor")
                {
                    this.FormProp.BgColor = dialogResult.Parameters["Value"]?.ToString() ?? "";
                    FormProp.BgImgUrl = "";
                    FormProp.BgImgName = "";
                    BtnRemoveVisiable = Visibility.Hidden;
                    MapImg = null;
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


        private ShapeBase _selectedItem = new ShapeBase();
        public ShapeBase SelectedItem
        {
            get { 
                this.IsShowFieldName = string.IsNullOrEmpty(_selectedItem.FieldName) ? Visibility.Hidden : Visibility.Visible;
                IsDateTime = _selectedItem.BaseType==DefaultConst.BaseType_DateTime ? Visibility.Visible : Visibility.Hidden;
                return _selectedItem; }
            set {
               
                SetProperty(ref _selectedItem, value); }
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

        private ObservableCollection<FieldModel> dtFormats = new ObservableCollection<FieldModel>();
        public ObservableCollection<FieldModel> DtFormats
        {
            get { return dtFormats; }
            set { SetProperty(ref dtFormats, value); }
        }

        private string _selectedField;
        public string SelectedField
        {
            get => _selectedField;
            set => SetProperty(ref _selectedField, value);
        }

        private bool _isShowForm;

        public bool IsShowForm
        {
            get => _isShowForm;
            set => SetProperty(ref _isShowForm, value);
        }
        private bool _isShowControl;

        public bool IsShowControl
        {
            get => _isShowControl;
            set => SetProperty(ref _isShowControl, value);
        }

        //是否显示绑定字段
        private Visibility _isShowFieldName;

        public Visibility IsShowFieldName
        {
            get => _isShowFieldName;
            set { _isShowFieldName = value; RaisePropertyChanged(); }
         
        }
        //是否显示日期控件
        private Visibility _isDateTime;

        public Visibility IsDateTime
        {
            get => _isDateTime;
            set { _isDateTime = value; RaisePropertyChanged(); }

        }

        private Visibility _visForm;

        public Visibility VisForm
        {
            get => _visForm;
            set => SetProperty(ref _visForm, value);
        }
        private Visibility _visControl;

        public Visibility VisControl
        {
            get => _visControl;
            set => SetProperty(ref _visControl, value);
        }
        private Visibility _btnRemoveVisiable;

        public Visibility BtnRemoveVisiable
        {
            get { return _btnRemoveVisiable; }
            set => SetProperty(ref _btnRemoveVisiable, value);
        }


        private BitmapImage _mapImg;

        public BitmapImage MapImg
        {
            get => _mapImg;
            set => SetProperty(ref _mapImg, value);
        }

        //表单属性
        private FormProp _formProp;

        public FormProp FormProp
        {
            get { return _formProp; }
            set => SetProperty(ref _formProp, value);
        }


        public DelegateCommand MouseDownCommand { get; set; }

        public DelegateCommand<string> GetColorCommand { get; set; }

        public DelegateCommand<string> ExecuteCommand { get; set; }

        public DelegateCommand<object> SelectionChangeCommand { get; set; }

        public DelegateCommand<object> DtFormatsChangeCommand { get; set; }

        public DelegateCommand<object> RadioButtonCommand { get; set; }

        public DelegateCommand<Object> RemoveBgCommand { get; set; }

        public DelegateCommand<Object> RemoveControlCommand { get; set; }
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
