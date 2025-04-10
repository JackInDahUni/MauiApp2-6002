using MauiApp2.ViewModel;
namespace MauiApp2;

public partial class CalendarPage : ContentPage
{
    public CalendarPage(CalendarViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}