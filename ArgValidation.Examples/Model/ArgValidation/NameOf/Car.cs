﻿using System;

namespace ArgValidation.Examples.Model.ArgValidation.NameOf
{
    public class Car
    {
        public CarModel Model { get; }
        public string Color { get; }
        public int ReleaseYear { get; }
        public DateTime DateOfPurchase { get; }
            
        public Car(CarModel model, string color, int releaseYear, DateTime dateOfPurchase, int? passangerCount)
        {
            Arg.NotNull(model, nameof(model));
            Arg.NotDefault(dateOfPurchase, nameof(dateOfPurchase));
            Arg.Positive(releaseYear, nameof(releaseYear));

            Arg.Validate(color, nameof(color))
                .NotNullOrWhitespace()
                .MaxLength(20);

            Arg.Validate(color, nameof(color))
                .CountMoreThan(1);

            Arg.IfNotNull(passangerCount, nameof(passangerCount))
                  .InRange(0, 10);

            Model = model;
            Color = color;
            ReleaseYear = releaseYear;
            DateOfPurchase = dateOfPurchase;
        }

        public override string ToString()
        {
            return $"{nameof(Model)}: {Model}, {nameof(ReleaseYear)}: {ReleaseYear}, {nameof(Color)}: {Color}";
        }
    }
}