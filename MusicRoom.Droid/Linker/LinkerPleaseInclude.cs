using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Input;
using Android.App;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MusicRoom.Droid.Linker
{
    // This class is never actually executed, but when Xamarin linking is enabled it does how to ensure types and properties
    // are preserved in the deployed app
    [Preserve(AllMembers = true)]
    public class LinkerPleaseInclude
    {
        public void Include(Button button)
        {
            button.Click += (s, e) => button.Text = $"{button.Text}";
        }

        public void Include(View view)
        {
            view.Click += (s, e) => view.ContentDescription = $"{view.ContentDescription}";
        }

        public void Include(TextView text)
        {
            text.AfterTextChanged += (sender, args) => text.Text = $"{text.Text}";
            text.Hint = $"{text.Hint}";
        }

        public void Include(CompoundButton cb)
        {
            cb.CheckedChange += (sender, args) => cb.Checked = !cb.Checked;
        }

        public void Include(SeekBar sb)
        {
            sb.ProgressChanged += (sender, args) => sb.Progress = sb.Progress + 1;
        }

        public void Include(RadioGroup radioGroup)
        {
            radioGroup.CheckedChange += (sender, args) => radioGroup.Check(args.CheckedId);
        }

        public void Include(RatingBar ratingBar)
        {
            ratingBar.RatingBarChange += (sender, args) => ratingBar.Rating = 0 + ratingBar.Rating;
        }

        public void Include(Activity act)
        {
            act.Title = $"{act.Title}";
        }

        public void Include(ICommand command)
        {
            command.CanExecuteChanged += (s, e) => { if (command.CanExecute(null)) command.Execute(null); };
        }

        public void Include(INotifyCollectionChanged changed)
        {
            changed.CollectionChanged += (s, e) => { _ = $"{e.Action}{e.NewItems}{e.NewStartingIndex}{e.OldItems}{e.OldStartingIndex}"; };
        }

        public void Include(INotifyPropertyChanged changed)
        {
            changed.PropertyChanged += (sender, e) => { _ = e.PropertyName; };
        }


    }
}
