public interface IPlayFeeCalculator
   {
       decimal CalculateTragedyPlayFee(Play play);
       decimal CalculateComedyPlayFee(Play play);
       decimal CalculateHistoricalPlayFee(Play play);
   }