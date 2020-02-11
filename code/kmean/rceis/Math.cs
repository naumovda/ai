using System; 
using System.Collections; 

///<Summary>
///C# Math Class for computing Standard Deviation, Normal Distribution, Probability Density
///Will Be Used In C# Naive Bayes Data Mining Algorithm for Classifying Numeric or Continuous Data
///</Summary>
///<Remarks>
///C# Probability Density Function or Normal Distribution from 
///http://www.experts-exchange.com/Programming/Programming_Languages/C_Sharp/Q_20936306.html 
///C# Mean and C# Standard Deviation Functions from Daniel Olson at
///http://authors.aspalliance.com/olson/
///</Remarks>
public class Maths 
{       
       
 ///<Summary>
///Calculates standard deviation of numbers in an ArrayList
///</Summary>       
 public static double StandardDeviation(ArrayList num) 
  { 
    double SumOfSqrs = 0; 
    double avg = Average(num); 
    for (int i=0; i<num.Count; i++) 
    { 
      SumOfSqrs += Math.Pow(((double)num[i] - avg), 2); 
    } 
    double n = (double)num.Count; 
    return Math.Sqrt(SumOfSqrs/(n-1)); 
  } 
    
    ///<Summary>
    ///Calculates standard deviation of numbers of doubles data type in an array
    ///</Summary>  
  public static double StandardDeviation(double[] num) 
  { 
    double Sum = 0.0, SumOfSqrs = 0.0; 
    for (int i=0; i<num.Length; i++) 
    { 
      Sum += num[i]; 
      SumOfSqrs += Math.Pow(num[i], 2); 
    } 
    double topSum = (num.Length * SumOfSqrs) - (Math.Pow(Sum, 2)); 
    double n = (double)num.Length; 
    return Math.Sqrt( topSum / (n * (n-1)) ); 
  } 
    
   ///<Summary>
  ///Calculates standard deviation of numbers of doubles data type in a column of an array
  ///</Summary>  
  public static double StandardDeviation(double[,] num, int col) 
  { 
    double Sum = 0.0, SumOfSqrs = 0.0; 
    int len = num.GetLength(0); 
    for (int i=0; i<len; i++) 
    { 
      Sum += num[i,col]; 
      SumOfSqrs += Math.Pow(num[i,col], 2); 
    } 
    double topSum = (len * SumOfSqrs) - (Math.Pow(Sum, 2)); 
    double n = System.Convert.ToDouble(len); 
    return Math.Sqrt( topSum / (n * (n-1)) ); 
  } 
  

	public static double Average(double[,] num, int col) 
	{ 
		double sum = 0.0; 
		int len = num.GetUpperBound(0) + 1; 
		for (int i=0; i<len; i++) 
		{ 
			sum += num[i,col]; 
		} 
		double avg = sum / System.Convert.ToDouble(len);

		return avg; 
	} 

  ///<Summary>
  ///Calculates average of numbers of doubles data type in an array 
  ///</Summary>  
  public static double Average(double[] num) 
  { 
    double sum = 0.0; 
    for (int i=0; i<num.Length; i++) 
    { 
      sum += num[i]; 
    } 
    double avg = sum / System.Convert.ToDouble(num.Length); 

    return avg; 
  } 
  
   ///<Summary>
  ///Calculates average of numbers of integer data type in an array 
  ///</Summary>    
  public static double Average(int[] num) 
  { 
    double sum = 0.0; 
    for (int i=0; i<num.Length; i++) 
    { 
      sum += num[i]; 
    } 
    double avg = sum / System.Convert.ToDouble(num.Length); 

    return avg; 
  } 
  
   ///<Summary>
  ///Calculates average of numbers of integer data type in an ArrayList
  ///</Summary>  
  public static double Average(ArrayList num) 
  { 
    double sum = 0.0; 
    for (int i=0; i<num.Count; i++) 
    { 
      sum += (double)num[i]; 
    } 
    double avg = sum / System.Convert.ToDouble(num.Count); 

    return avg; 
  } 

	public static double Min(double[,] num, int col) 
	{ 
		double min = 0.0; 
		int len = num.GetUpperBound(0) + 1; 
		for (int i=0; i<len; i++) 
		{ 
			if (min > num[i,col])
				 min = num[i,col];
		} 		
		return min; 
	} 

	public static double Max(double[,] num, int col) 
	{ 
		double max = 0.0; 
		int len = num.GetUpperBound(0) + 1; 
		for (int i=0; i<len; i++) 
		{ 
			if (max < num[i,col])
				max = num[i,col];
		} 		
		return max; 
	} 
	/// <summary> 
    /// Calculates Normal Distribution or Probability Density given the mean, and standard deviation 
    /// </summary> 
    /// <param name="x">The value for which you want the distribution.</param> 
    /// <param name="mean">The arithmetic mean of the distribution.</param> 
    /// <param name="deviation">The standard deviation of the distribution.</param> 
    /// <returns>Returns the normal distribution for the specified mean and standard deviation.</returns> 
    public static double NormalDistribution(double x, double mean, double deviation) 
    { 
        return NormalDensity(x, mean, deviation); 
    } 
    
	private static double NormalDensity(double x, double mean, double deviation) 
    { 
      return Math.Exp(-(Math.Pow((x - mean)/deviation, 2)/2))/Math.Sqrt(2*Math.PI)/deviation; 
    }   

	public static void LinearRegression(double[,] num, int colx, int coly, out double a, out double b) 
	{ 
		double  sum_x  = 0.0, 
				sum_y  = 0.0,
				sum_x2 = 0.0,
				sum_yx = 0.0; 

		int len = num.GetUpperBound(0) + 1; 
		for (int i=0; i<len; i++) 
		{ 
			sum_x  += num[i,colx];
			sum_x  += num[i,coly];
			sum_x2 += num[i,colx]*num[i,colx];
			sum_yx += num[i,coly]*num[i,colx];
		} 		

		double d0 = len * sum_x2 - sum_x * sum_x;
		double d1 = sum_y * sum_x2 - sum_x * sum_yx;
		double d2 = len * sum_yx - sum_y * sum_x;

		a = d1 / d0;
		b = d2 / d0;
	} 
} 

