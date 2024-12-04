using System.Collections.ObjectModel;
using Todo.DragDrop.Models;

namespace Todo.ViewModels.Duty
{
    public class TemplateViewModel : BindableBase, IDialogAware
    {
        public TemplateViewModel()
        {
            ToolItems = new ObservableCollection<ShapeBase>()
               {
                   new RectangleBaseToolItem(){Width=20,Height=20},
                     new RectangleBaseToolItem(){Width=20,Height=20},
                       new RectangleBaseToolItem(){Width=20,Height=20}
               };
            MouseDownCommand = new DelegateCommand(CanvasMouseDown);
        }

        private void CanvasMouseDown()
        {
            var aaa = "";
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


        private ObservableCollection<ShapeBase> _items = new ObservableCollection<ShapeBase>();
        public ObservableCollection<ShapeBase> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }

        public DelegateCommand MouseDownCommand;
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
