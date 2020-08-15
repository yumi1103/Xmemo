using System;
namespace Xmemo.Droid
{
    public class MyDatePickerEventArgs: EventArgs
    {
        public DateTime SelectedDate { get; private set; }

        public MyDatePickerEventArgs(DateTime selectedDate)
        {
            SelectedDate = selectedDate;
        }
    }
}
