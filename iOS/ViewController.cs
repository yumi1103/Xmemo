using System;
using UIKit;
using System.Diagnostics;
using Foundation;

namespace Xmemo.iOS
{
    public partial class ViewController : UIViewController
    {
        int count = 1;

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            MemoHolder.Current.Memo = new Memo()
            {
                Date = DateTime.Now,
                Subject = "",
                Text = "",
            };

            DisplayMemo();

            SubjectText.EditingChanged += (s, e) =>
            {
                MemoHolder.Current.Memo.Subject = SubjectText.Text;
            };

            MemoText.EditingChanged += (s, e) =>
            {
                MemoHolder.Current.Memo.Text = MemoText.Text;
            };

            SetupDatePicker();
        }

        private void DisplayMemo()
        {
            var memo = MemoHolder.Current.Memo;

            DateText.Text = string.Format("{0:yyyy/MM/dd}", memo.Date);
            SubjectText.Text = memo.Subject;
            MemoText.Text = memo.Text;
        }

        private void SetupDatePicker()
        {
            var doneButton = new UIBarButtonItem("閉じる", UIBarButtonItemStyle.Done, (s, e) =>
             {
                 //DateTextのDatePickerを閉じる
                 DateText.ResignFirstResponder();
             });

            //閉じるボタンを乗せるツールバー
            var toolbar = new UIToolbar()
            {
                BarStyle = UIBarStyle.Default,
                Translucent = true,
                TintColor = null,
            };
            toolbar.SizeToFit();
            toolbar.SetItems(new[]
            {
                new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace),
                doneButton,
            }, true);

            //DatePicker
            var datePicker = new UIDatePicker()
            {
                Mode = UIDatePickerMode.Date,
                Locale = new NSLocale("ja_JP"),
            };
            datePicker.ValueChanged += (s, e) =>
            {                
                MemoHolder.Current.Memo.Date = (DateTime)datePicker.Date;
                DisplayMemo();
            };

            //DateTextに作成したツールバーとDatePickerをセットする
            DateText.InputAccessoryView = toolbar;
            DateText.InputView = datePicker;
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.		
        }
    }
}
