// ***********************************************************************
//  Assembly          : RzR.Shared.Attributes.AttributeValidator.Mvvm
//  Author            : RzR
//  Created           : 17-07-2026 23:07
// 
//  Last Modified By : RzR
//  Last Modified On : 18-07-2026 14:43
//  ***********************************************************************
//  <copyright file="ValidatableObservableObject.cs" company="RzR SOFT & TECH">
//      Copyright (c) RzR. All rights reserved.
//  </copyright>
//  <contact>
//      https://iamrzr.dev/contact
//  </contact>
//  <summary></summary>
//  ***********************************************************************

#region U S I N G

using RzR.Validation.Attributes.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;

#endregion

namespace RzR.Validation.Attributes.Mvvm;

/// -------------------------------------------------------------------------------------------------
/// <summary>
///     Abstract base class that combines <see cref="INotifyPropertyChanged" /> and
///     <see cref="INotifyDataErrorInfo" /> to support attribute-based validation in MVVM view
///     models.
///     Validation is driven by <see cref="System.ComponentModel.DataAnnotations.Validator" /> so
///     all
///     <c>Val*</c> attributes from <c>RzR.Validation.Attributes</c> work out of the box.
/// </summary>
/// <remarks>
///     This class has no dependency on WPF, WinUI, MAUI, or Avalonia. It relies solely on
///     <c>System.ComponentModel</c> and <c>System.ComponentModel.DataAnnotations</c>, both of
///     which
///     are available on netstandard2.0 and later. Any MVVM host that binds to
///     <see cref="INotifyDataErrorInfo" /> (WPF, WinUI 3, .NET MAUI, Avalonia) can use it
///     directly.
/// </remarks>
/// <seealso cref="T:System.ComponentModel.INotifyPropertyChanged"/>
/// <seealso cref="T:System.ComponentModel.INotifyDataErrorInfo"/>
/// =================================================================================================
public abstract class ValidatableObservableObject : INotifyPropertyChanged, INotifyDataErrorInfo
{
    private readonly Dictionary<string, List<string>> _errorsByProperty = new();

    /// <inheritdoc />
    public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Gets a value indicating whether any property currently has validation errors.
    /// </summary>
    /// <value>
    ///     true if the entity currently has validation errors; otherwise, false.
    /// </value>
    /// =================================================================================================
    public bool HasErrors => _errorsByProperty.Count > 0;

    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Returns the validation error messages for the specified property, or all errors when
    ///     <paramref name="propertyName" /> is <c>null</c> or empty.
    /// </summary>
    /// <param name="propertyName">
    ///     The name of the property to retrieve errors for. Pass <c>null</c> or an empty string to
    ///     retrieve every recorded error across all properties.
    /// </param>
    /// <returns>
    ///     An enumerable of <see cref="string" /> error messages, or an empty enumerable when there
    ///     are no errors for the requested property.
    /// </returns>
    /// =================================================================================================
    public IEnumerable GetErrors(string propertyName)
    {
        if (string.IsNullOrEmpty(propertyName))
            return _errorsByProperty.Values.SelectMany(e => e);

        return _errorsByProperty.TryGetValue(propertyName, out var errors)
            ? errors
            : Enumerable.Empty<string>();
    }

    /// <inheritdoc />
    public event PropertyChangedEventHandler PropertyChanged;

    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Sets a backing field to <paramref name="value" />, raises <see cref="PropertyChanged" />,
    ///     and immediately validates the property using its <see cref="ValidationAttribute" />
    ///     decorations.
    /// </summary>
    /// <typeparam name="T">The type of the property.</typeparam>
    /// <param name="field">[in,out] Reference to the backing field.</param>
    /// <param name="value">The new value to assign.</param>
    /// <param name="propertyName">
    ///     (Optional)
    ///     The property name, resolved automatically by <see cref="CallerMemberNameAttribute" />.
    /// </param>
    /// <returns>
    ///     <c>true</c> when the value changed and notifications were raised; <c>false</c> when the
    ///     value was equal to the existing field value and nothing was updated.
    /// </returns>
    /// =================================================================================================
    protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
            return false;

        field = value;
        OnPropertyChanged(propertyName);
        ValidateProperty(value, propertyName);

        return true;
    }

    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Raises <see cref="PropertyChanged" /> for the specified property.
    /// </summary>
    /// <param name="name">
    ///     (Optional)
    ///     The property name, resolved automatically by <see cref="CallerMemberNameAttribute" />.
    /// </param>
    /// =================================================================================================
    protected void OnPropertyChanged([CallerMemberName] string name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Validates a single property value against all <see cref="ValidationAttribute" />
    ///     decorations on that property and updates the internal error store accordingly.
    /// </summary>
    /// <param name="value">The current value of the property to validate.</param>
    /// <param name="propertyName">The name of the property being validated.</param>
    /// =================================================================================================
    public void ValidateProperty(object value, string propertyName)
    {
        if (string.IsNullOrEmpty(propertyName))
            return;

        var context = new ValidationContext(this) { MemberName = propertyName };
        var results = new List<ValidationResult>();

        Validator.TryValidateProperty(value, context, results);

        if (results.Count > 0)
        {
            _errorsByProperty[propertyName] = results
                .Select(r => r.ErrorMessage ?? string.Empty)
                .ToList();
        }
        else
            _errorsByProperty.Remove(propertyName);

        RaiseErrorsChanged(propertyName);
    }

    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Validates the entire object using Validator.TryValidateObject with
    ///     <c>validateAllProperties: true</c>, which covers property-level, class-level, and
    ///     cross-property attributes (e.g. conditional and mutually-exclusive rules). Refreshes the
    ///     per-property error store and raises <see cref="ErrorsChanged" /> for every property whose
    ///     error state changes.
    /// </summary>
    /// <returns>
    ///     <c>true</c> when the object is fully valid (no errors on any property); <c>false</c>
    ///     when one or more properties have errors.
    /// </returns>
    /// =================================================================================================
    public bool ValidateAll()
    {
        this.TryValidate(out var results);

        var previousKeys = _errorsByProperty.Keys.ToList();
        _errorsByProperty.Clear();

        foreach (var result in results)
        {
            var memberNames = result.MemberNames?.ToList() ?? new List<string>();
            if (memberNames.Count == 0)
                memberNames.Add(string.Empty);

            foreach (var memberName in memberNames)
            {
                if (!_errorsByProperty.TryGetValue(memberName, out var bucket))
                {
                    bucket = new List<string>();
                    _errorsByProperty[memberName] = bucket;
                }

                bucket.Add(result.ErrorMessage ?? string.Empty);
            }
        }

        var changedKeys = previousKeys
            .Union(_errorsByProperty.Keys)
            .Distinct();

        foreach (var key in changedKeys)
            RaiseErrorsChanged(key);

        return !HasErrors;
    }

    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Raises the errors changed event.
    /// </summary>
    /// <param name="propertyName">The name of the property being validated.</param>
    /// =================================================================================================
    private void RaiseErrorsChanged(string propertyName)
        => ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
}