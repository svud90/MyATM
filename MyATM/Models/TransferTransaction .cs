using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyATM.Models
{

    public class TransferTransaction
    {

        [Required]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }


        [Required]
        [StringLength(10)]
        [RegularExpression(@"\d{6,10}", ErrorMessage = "Account # must be between 6 and 10 digists.")]
        [Display(Name="To Account #")]
        public string RecievingCheckingAccountNumber { get; set; }


    }
        
}