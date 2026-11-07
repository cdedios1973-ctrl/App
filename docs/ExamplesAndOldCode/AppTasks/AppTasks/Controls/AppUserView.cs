namespace AppTasks.Controls
{
    public class AppUserView : ContentView
    {
        public static readonly BindableProperty IdProperty = BindableProperty.Create(nameof(Id), typeof(string), typeof(AppUserView), string.Empty);
        public static readonly BindableProperty NameProperty = BindableProperty.Create(nameof(Name), typeof(string), typeof(AppUserView), string.Empty);
        public static readonly BindableProperty SurnameProperty = BindableProperty.Create(nameof(Surname), typeof(string), typeof(AppUserView), string.Empty);
        public static readonly BindableProperty UserColorProperty = BindableProperty.Create(nameof(UserColor), typeof(Color), typeof(AppUserView), Colors.White);
        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(AppUserView), Colors.DarkGray);
        public static readonly BindableProperty IconImageSourceProperty = BindableProperty.Create(nameof(IconImageSource), typeof(ImageSource), typeof(AppUserView), default(ImageSource));
        public static readonly BindableProperty IconBackgroundColorProperty = BindableProperty.Create(nameof(IconBackgroundColor), typeof(Color), typeof(AppUserView), Colors.LightGray);

        public string Id
        {
            get => (string)GetValue(IdProperty);
            set => SetValue(IdProperty, value);
        }

        public string Name
        {
            get => (string)GetValue(NameProperty);
            set => SetValue(NameProperty, value);
        }

        public string Surname
        {
            get => (string)GetValue(SurnameProperty);
            set => SetValue(SurnameProperty, value);
        }

        public Color UserColor
        {
            get => (Color)GetValue(UserColorProperty);
            set => SetValue(UserColorProperty, value);
        }

        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        public ImageSource IconImageSource
        {
            get => (ImageSource)GetValue(IconImageSourceProperty);
            set => SetValue(IconImageSourceProperty, value);
        }

        public Color IconBackgroundColor
        {
            get => (Color)GetValue(IconBackgroundColorProperty);
            set => SetValue(IconBackgroundColorProperty, value);
        }
    }
}
