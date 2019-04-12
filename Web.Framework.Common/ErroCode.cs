using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Framework.Common
{
    public class ErroCode
    {
        string ErrroMesg = string.Empty;
        public string GettingError(string Code)
        {
            switch (Code)
            {
                case "0":
                    ErrroMesg = "Successfully completed";
                    break;
                case "1":
                    ErrroMesg = "Session with this number already exists. ";
                    break;
                case "2":
                    ErrroMesg = "Invalid Dealer code.";
                    break;
                case "3":
                    ErrroMesg = "Invalid Dealer code.";
                    break;
                case "4":
                    ErrroMesg = "Invalid Operator code.";
                    break;
                case "5":
                    ErrroMesg = "Invalid session code format";
                    break;
                case "6":
                    ErrroMesg = "Invalid EDS.";
                    break;
                case "7":
                    ErrroMesg = "Invalid amount format or amount value is out of admissible range.";
                    break;
                case "8":
                    ErrroMesg = "Invalid phone number format.";
                    break;
                case "9":
                    ErrroMesg = "Invalid format of personal account number.";
                    break;
                case "10":
                    ErrroMesg = "Invalid request message format.";
                    break;
                case "11":
                    ErrroMesg = "Session with such a number does not exist.";
                    break;
                case "12":
                    ErrroMesg = "The request is made from an unregistered IP.";
                    break;
                case "13":
                    ErrroMesg = "The outlet is not registered with the Service Provider.";
                    break;
                case "15":
                    ErrroMesg = "Payments to the benefit of this operator are not supported by the system.";
                    break;
                case "17":
                    ErrroMesg = "The phone number does not match the previously entered number.";
                    break;
                case "18":
                    ErrroMesg = "The payment amount does not match the previously entered amount.";
                    break;
                case "19":
                    ErrroMesg = "The account number does not match the previously entered number.";
                    break;
                case "20":
                    ErrroMesg = "The payment is being completed.";
                    break;
                case "21":
                    ErrroMesg = "Not enough funds for effecting the payment.";
                    break;
                case "22":
                    ErrroMesg = "The payment has not been accepted. Funds transfer error.";
                    break;
                case "23":
                    ErrroMesg = "Invalid Mobile Number. Make sure your number belongs to this provider";
                    break;
                case "24":
                    ErrroMesg = "Error of connection with the provider’s server. please try it again";
                    break;
                case "25":
                    ErrroMesg = "Effecting of this type of payments is suspended";
                    break;
                case "26":
                    ErrroMesg = "Payments of this Dealer are temporarily blocked.";
                    break;
                case "27":
                    ErrroMesg = "Operations with this account are suspended";
                    break;
                case "30":
                    ErrroMesg = "General system failure.";
                    break;
                case "31":
                    ErrroMesg = "Exceeded number of simultaneously processed requests.";
                    break;
                case "32":
                    ErrroMesg = "Repeated payment within 60 minutes from the end of payment effecting process There";
                    break;
                case "33":
                    ErrroMesg = "This denomination is applicable in <Flexi OR Special> category";
                    break;
                case "34":
                    ErrroMesg = "Transaction with such number could not be found.";
                    break;
                case "35":
                    ErrroMesg = "Payment status alteration error.";
                    break;
                case "36":
                    ErrroMesg = "Invalid payment status. ";
                    break;
                case "37":
                    ErrroMesg = "An attempt of referring to the gateway that is different from the gateway at the previous";
                    break;
                case "38":
                    ErrroMesg = "Invalid date. The effective period of the payment may have expired.";
                    break;
                case "39":
                    ErrroMesg = "Invalid account number.";
                    break;
                case "40":
                    ErrroMesg = "The card of the specified value is not registered in the system";
                    break;
                case "41":
                    ErrroMesg = "Error in saving the payment in the system.";
                    break;
                case "42":
                    ErrroMesg = "Error in saving the receipt to the database.";
                    break;
                case "43":
                    ErrroMesg = "Your working session in the system is invalid (the duration of the session may have expired), try to re-enter.";
                    break;
                case "44":
                    ErrroMesg = "The Client cannot operate on this trading server.";
                    break;
                case "45":
                    ErrroMesg = "No license is available for accepting payments to the benefit of this operator.";
                    break;
                case "46":
                    ErrroMesg = "Could not complete the erroneous payment.";
                    break;
                case "47":
                    ErrroMesg = "Time limitation of access rights has been activated.";
                    break;
                case "48":
                    ErrroMesg = "Error in saving the session data to the database.";
                    break;
                case "50":
                    ErrroMesg = "Effecting payments in the system is temporarily impossible.";
                    break;
                case "51":
                    ErrroMesg = "Data are not found in the system.";
                    break;
                case "52":
                    ErrroMesg = "The Dealer may be blocked. The Dealer’s current status does not allow effecting payments";
                    break;
                case "53":
                    ErrroMesg = "The Acceptance Outlet may be blocked. The Acceptance Outlet’s current status does not allow effecting payments.";
                    break;
                case "54":
                    ErrroMesg = "The Operator may be blocked. The Operator’s current status does not allow effecting payments";
                    break;
                case "55":
                    ErrroMesg = "The Dealer Type does not allow effecting payments.";
                    break;
                case "56":
                    ErrroMesg = "An Acceptance Outlet of another type was expected. This Acceptance Outlet type does not allow effecting payments.";
                    break;
                case "57":
                    ErrroMesg = "An Operator of another type was expected. This Operator type does not allow effecting payments";
                    break;
                case "81":
                    ErrroMesg = "Exceeded the maximum payment amount.";
                    break;
                case "82":
                    ErrroMesg = "Daily debit amount has been exceeded";
                    break;
                case "83":
                    ErrroMesg = "Maximum payment amount for the outlet has been exceeded.";
                    break;
                case "84":
                    ErrroMesg = "Daily total amount for the outlet has been exceeded.";
                    break;
                case "85":
                    ErrroMesg = "AMOUNT ALL is invalid";
                    break;
                case "86":
                    ErrroMesg = "Invalid rate value";
                    break;
                case "87":
                    ErrroMesg = "Beneficiary is blocked";
                    break;
                case "88":
                    ErrroMesg = "Duplicate Transaction (Same Mobile Number) Duplicate";
                    break;
                case "89":
                    ErrroMesg = "A limit by a beneficiary is reached.";
                    break;
                case "224":
                    ErrroMesg = "Invalid Mobile Number";
                    break;
                case "333":
                    ErrroMesg = "Unknown error.";
                    break;
                case "171":
                    ErrroMesg = "Manually Cancelled";
                    break;
            }
            return ErrroMesg;
        }
    }
}
