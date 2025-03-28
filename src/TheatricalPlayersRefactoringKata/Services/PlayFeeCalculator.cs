public class PlayFeeCalculator : IPlayFeeCalculator
   {
       public decimal CalculateTragedyPlayFee(Play play)
       {
           // Implement tragedy play fee calculation logic
       }

       public decimal CalculateComedyPlayFee(Play play)
       {
           // Implement comedy play fee calculation logic
       }

       public decimal CalculateHistoricalPlayFee(Play play)
       {
           var tragedyFee = CalculateTragedyPlayFee(play);
           var comedyFee = CalculateComedyPlayFee(play);
           return tragedyFee + comedyFee;
       }
   }