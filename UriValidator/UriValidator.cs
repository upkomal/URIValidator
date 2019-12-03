using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace UriValidator {

    /// <summary>
    ///Enum to define type of URI can send by User:
    /// </summary>
  public enum UriValidatorRules {
    Wildcard,
    Absolute,
    Regex
  }

    /// <summary>
    /// This Method accept string URI to validate in the set of validation rules in IEnumrable string:As a result it send a bool.
    /// </summary>
  public class UriValidator {
    public static bool Validate(string uri, IEnumerable<string> rules) {
      if(string.IsNullOrEmpty(uri)) return false;
      bool result = false;
      foreach(var rule in rules) {
        if(rule.StartsWith("@")) {                      //Check for RegEx and call Method:
          result = ValidateRegexUri(uri, rule);
        } else if(rule.Contains("*")) {
          result = ValidateWildcardUri(uri, rule);     //Check for Wildcard and call Method:
                } else {
          result = ValidateAbsoluteUri(uri, rule);     //Check for Absolute and call Method:
                }
        if(result) return true;
      }
      return result;
    }
 
        /// <summary>
        /// Methos to Match uri with pattern specified in rule:
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="rule"></param>
        /// <returns>True/False</returns>
    private static bool ValidateRegexUri(string uri, string rule) {
      string regex = rule.Substring(1, rule.Length - 1);
      var result = new Regex(regex).Match(uri);
      return result.Success;
    }

        /// <summary>
        /// Method to validate wildcard URI :
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="rule"></param>
        /// <returns>True/False</returns>

    private static bool ValidateWildcardUri(string uri, string rule) {
      string regex = rule.Replace("/","\\/").Replace("*", "[\\w]+");
      return Regex.Match(uri, regex).Success;
    }

        /// <summary>
        /// Method to check Absolute URI:
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="rule"></param>
        /// <returns>True/False</returns>
    private static bool ValidateAbsoluteUri(string uri, string rule) {
      return rule.ToLower().Equals(uri.ToLower());
    }
  }
}
