﻿using System;
using System.Collections.Generic;
using System.Linq;
using DropZone.Annotations;
using DropZone.DependencyService;
using DropZone.Models;
using DropZone.Repository;
using DropZone.ViewModels;
using Xamarin.Forms;

namespace DropZone.Views
{
    /// <summary>
    /// Represents a jump page.
    /// </summary>
    public class JumpPage : XForms.Toolkit.Mvvm.BaseView
    {
        private readonly IGalleryImageService _galleryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="JumpPage"/> class.
        /// </summary>
        public JumpPage([NotNull] JumpViewModel viewModel, [NotNull] IRepository repository)
        {
            if (viewModel == null) throw new ArgumentNullException("viewModel");
            if (repository == null) throw new ArgumentNullException("repository");

            ConfigureToolbar();
            ConfigureContent(viewModel, repository);
            ConfigureViewModel(viewModel);
            _galleryService = Xamarin.Forms.DependencyService.Get<IGalleryImageService>();
            
            Appearing += OnAppearing;
            Disappearing += OnDisappearing;
        }

        private void OnAppearing(object sender, EventArgs e)
        {
            ToolbarItems.Add(new ToolbarItem("Save", string.Empty, Save));
        }

        private async void Save()
        {
            await ((JumpViewModel)BindingContext).Save();
            await Navigation.PopAsync();
        }

        private void OnDisappearing(object sender, EventArgs e)
        {
            _galleryService.ImageSelected -= OnImageSelected;
            ToolbarItems.Clear();
        }

        private void ConfigureViewModel(JumpViewModel viewModel)
        {
            BindingContext = viewModel;            
        }

        private void ConfigureContent(JumpViewModel viewModel, IRepository repository)
        {
            Grid grid = new Grid
            {
                Padding = new Thickness(20, 0, 20, 0),
                RowDefinitions = new RowDefinitionCollection
                {
                    new RowDefinition(),
                    new RowDefinition(),
                    new RowDefinition(),
                    new RowDefinition(),
                    new RowDefinition(),
                    new RowDefinition(),
                    new RowDefinition(),
                    new RowDefinition(),
                    new RowDefinition(),
                    new RowDefinition(),
                    new RowDefinition(),
                    new RowDefinition()
                },
                ColumnDefinitions = new ColumnDefinitionCollection
                {
                    new ColumnDefinition{ Width= new GridLength(100, GridUnitType.Star) }
                },
            };
            Entry jumpNumber = new Entry
            {
                Placeholder = "Jump Number",
                HorizontalOptions= LayoutOptions.Fill,
                VerticalOptions= LayoutOptions.Fill,
            };
            jumpNumber.SetBinding(Entry.TextProperty, "JumpNumber");
            grid.Children.Add(jumpNumber, 0, 0);

            DatePicker datePicker = new DatePicker();
            datePicker.SetBinding(DatePicker.DateProperty, "JumpDate");
            grid.Children.Add(datePicker, 0, 1);

            Entry location = new Entry
            {
                Placeholder = "Location",
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
            };
            location.SetBinding(Entry.TextProperty, "Location");
            grid.Children.Add(location, 0, 2);
            
            Picker aircraftPicker = CreateAircraftPicker(viewModel, repository);
            grid.Children.Add(aircraftPicker, 0, 3);

            Entry altitude = new Entry
            {
                Placeholder = "Altitude (ft)",
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Keyboard = Keyboard.Numeric
            };
            altitude.SetBinding(Entry.TextProperty, "Altitude");
            grid.Children.Add(altitude, 0, 4);

            Picker jumpTypePicker = CreateJumpTypePicker(viewModel, repository);
            grid.Children.Add(jumpTypePicker, 0, 5);

            Entry freefallDelay = new Entry
            {
                Placeholder = "Freefall Delay (seconds)",
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Keyboard = Keyboard.Numeric
            };
            freefallDelay.SetBinding(Entry.TextProperty, "FreefallDelay");
            grid.Children.Add(freefallDelay, 0, 6);

            Entry totalTime = new Entry
            {
                Placeholder = "Total Time (seconds)",
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Keyboard = Keyboard.Numeric
            };
            totalTime.SetBinding(Entry.TextProperty, "TotalTime");
            grid.Children.Add(totalTime, 0, 7);

            Entry container = new Entry
            {
                Placeholder = "Container",
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
            };
            container.SetBinding(Entry.TextProperty, "Container");
            grid.Children.Add(container, 0, 8);

            Entry description = new Entry
            {
                Placeholder = "Description",
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            description.SetBinding(Entry.TextProperty, "Description");
            grid.Children.Add(description, 0, 9);

            Button addImage = new Button{ Text = "Select Image" };

            Image image = new Image();
            image.SetBinding(Image.SourceProperty, "ThumbnailImage");

            addImage.Clicked += (sender, args) =>
            {
                _galleryService.ImageSelected += OnImageSelected;
                _galleryService.SelectImage();
            };

            grid.Children.Add(addImage, 0, 10);

            grid.Children.Add(image, 0, 11);
            
            ScrollView scrollView = new ScrollView
            {
                Content = new StackLayout
                {
                    Children =
                    {
                        grid
                    }
                }
            };
            Content = scrollView;
        }

        // TODO: Change this to bind to view model. The Picker class is not yet bindable. 8/6/2014
        // http://forums.xamarin.com/discussion/17875/binding-to-picker-items
        private static Picker CreateAircraftPicker(JumpViewModel viewModel, IRepository repository)
        {
            Picker aircraftPicker = new Picker
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Title = "Aircraft",
            };

            IEnumerable<IAircraft> allAircraft = repository.LoadAllAircraft();

            foreach (IAircraft aircraft in allAircraft)
            {
                aircraftPicker.Items.Add(aircraft.Name);
            }
            
            aircraftPicker.SelectedIndex = aircraftPicker.Items.IndexOf(viewModel.Aircraft.Name);
            aircraftPicker.SelectedIndexChanged += (sender, args) =>
            {
                viewModel.Aircraft = allAircraft.First(aircraft => aircraft.Name ==
                                allAircraft.ElementAt(aircraftPicker.SelectedIndex).Name);
            };
            return aircraftPicker;
        }

        // TODO: Change this to bind to view model. The Picker class is not yet bindable. 9/6/2014
        // http://forums.xamarin.com/discussion/17875/binding-to-picker-items
        private static Picker CreateJumpTypePicker(JumpViewModel viewModel, IRepository repository)
        {
            Picker jumpTypePicker = new Picker
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Title = "Jump Type",
            };

            IEnumerable<IJumpType> allJumpTypes = repository.LoadAllJumpTypes();

            foreach (IJumpType jumpType in allJumpTypes)
            {
                jumpTypePicker.Items.Add(jumpType.Name);
            }
            
            jumpTypePicker.SelectedIndex = jumpTypePicker.Items.IndexOf(viewModel.JumpType.Name);
            jumpTypePicker.SelectedIndexChanged += (sender, args) =>
            {
                viewModel.JumpType = allJumpTypes.First(jumpType => jumpType.Name ==
                                allJumpTypes.ElementAt(jumpTypePicker.SelectedIndex).Name);
            };
            return jumpTypePicker;
        }

        private void ConfigureToolbar()
        {
            Title = "Add Jump";
        }

        private void OnImageSelected(object sender, ImageSourceEventArgs e)
        {
            ((JumpViewModel)BindingContext).UpdateImage(e.Image);
        }
    }
}
