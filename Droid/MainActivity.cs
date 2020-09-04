using Android.App;
using Android.Widget;
using Android.OS;
using System;

namespace Xmemo.Droid
{
    [Activity(Label = "Xmemo", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);


            // メモの初期値的なやつ インスタンスではなくシンタックスシュガー
            MemoHolder.Current.Memo = new Memo()
            {
                Date = DateTime.Now,
                Subject = "",
                Text = "",
            };

            DisplayMemo();


            //件名の修正
            var subjectText = FindViewById<EditText>(Resource.Id.SubjectText);
            subjectText.TextChanged += (s, e) =>
            {
                MemoHolder.Current.Memo.Subject = subjectText.Text;
            };

            //内容の修正
            var memoText = FindViewById<EditText>(Resource.Id.MemoText);
            memoText.TextChanged += (s, e) =>
            {
                MemoHolder.Current.Memo.Text = memoText.Text;
            };

            //日付の追加
            FindViewById<EditText>(Resource.Id.DateText).Click += DateText_Click;

            var saveButton = FindViewById<Button>(Resource.Id.LoadButton);
            saveButton.Click += async (s, e) =>
            {
                await MemoHolder.Current.SaveAsync();
            };

            var loadButton = FindViewById<Button>(Resource.Id.LoadButton);
            loadButton.Click += async (s, e) =>
            {
                await MemoHolder.Current.LoadAsync();
                DisplayMemo();
            };

        }

        //メモの描画
        private void DisplayMemo()
        {
            var memo = MemoHolder.Current.Memo;
            FindViewById<EditText>(Resource.Id.DateText).Text = string.
                Format("{0:yyyy/MMM/dd}", memo.Date);
            FindViewById<EditText>(Resource.Id.SubjectText).Text = memo.Subject;
            FindViewById<EditText>(Resource.Id.MemoText).Text = memo.Text;
        }

        [Obsolete]
        private void DateText_Click(object sender, EventArgs e)
        {
            var datePicker = new MyDatePicker();
            datePicker.InitialDate = MemoHolder.Current.Memo.Date;
            datePicker.Selected += (s2, e2) =>
            {
                MemoHolder.Current.Memo.Date = e2.SelectedDate;
                DisplayMemo();
            };

            datePicker.Show(FragmentManager, "tag");
        }
    }
}

