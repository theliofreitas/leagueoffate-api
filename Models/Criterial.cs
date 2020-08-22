using System.ComponentModel.DataAnnotations;
using System;

namespace LeagueOfFateApi.Models 
{
  public class Criterial {
    [Required]
    public string Field { get; set; }

    private string _Operator;

    [Required]
    public string Operator { 
      get {
        return this._Operator;
      } 
      set {
        if (Enum.IsDefined(typeof(Operator), value)) {
          this._Operator = value;
        }
        else {
         // TODO: throw new Exception
         this._Operator = null;
        }
      }
    }

    [Required]
    [RegularExpression(@"^[A-Za-z0-9_]*$", ErrorMessage="Please enter a valid value for the 'value' field")]
    public string Value { get; set; }

    public bool Result { get; set; }
  }

  public enum Operator {
    equal_to,
    greater_than,
    lower_than
  }
}