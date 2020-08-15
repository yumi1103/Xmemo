using System;
using Android.App;
using Android.OS;
using Android.Widget;

namespace Xmemo.Droid
{
    [Obsolete]
    public class MyDatePicker: DialogFragment, DatePickerDialog.IOnDateSetListener
    {
        public DateTime InitialDate { get; set; }

        public event EventHandler<MyDatePickerEventArgs> Selected;

        //DatePickerDialogを生成してDialogFragmentに返す
        //日付の初期値をセットする
        //①
        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            return new DatePickerDialog(
                Activity, this, InitialDate.Year, InitialDate.Month - 1, InitialDate.Day);
        }

        //日付が選択されるとDialogFragmentから呼び出される
        //Selectedイベントを発行する
        //②
        public void OnDateSet(DatePicker view, int year, int month, int dayOfMonth)
        {
            var selectedDate = new DateTime(year, month + 1, dayOfMonth);
            Selected?.Invoke(this, new MyDatePickerEventArgs(selectedDate));
        }


        public MyDatePicker()
        {
        }
    }
}
