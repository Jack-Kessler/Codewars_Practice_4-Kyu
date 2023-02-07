//-----------------------------------------------------------------------------------------------------------------------------
// Sum of Intervals (4-Kyu) (Completed 2/6/2023 - 15th Day of Class)

//**** Efficient Method ****

//(int, int)[] intervals = {

//   (-7, 8), (-2, 10), (5, 15), (2000, 3150), (-5400, -5338)

////(-35, -25),
////(178, 179),
////(422, 436),
////(-358, -349),
////(403, 416),
////(125, 131),
////(462, 479),
////(305, 319),
////(-338, -328),
////(248, 257),
////(-378, -359),
////(-234, -214),
////(164, 170),
////(-368, -348)

////expected 150

////was 159
//    };

//int i = 0;

//(int, int)[] intervals2 = new (int, int)[intervals.Length];

//for (i = 0; i < intervals.Length; i++)
//{
//    intervals2[i].Item1 = intervals[i].Item1;
//    intervals2[i].Item2 = intervals[i].Item2;
//    //Console.WriteLine(intervals2[i].Item1);
//    //Console.WriteLine(intervals2[i].Item2);
//}

//intervals2 = intervals2.OrderBy(t => t.Item1).ThenBy(t => t.Item2).ToArray();

////for (i = 0; i < intervals2.Length; i++)
////{
////    //    Console.WriteLine(intervals2[i].Item1);
////    //    Console.WriteLine(intervals2[i].Item2);
////    //Console.WriteLine(intervals2[i]);
////}

//for (i = 0; i + 1 < intervals2.Length; i++)
//{
//    if (intervals2[i].Item2 > intervals2[i + 1].Item1)
//    {
//        intervals2[i + 1].Item1 = intervals2[i].Item2;

//        if (intervals2[i + 1].Item1 > intervals2[i + 1].Item2)
//        {
//            intervals2[i + 1].Item2 = intervals2[i + 1].Item1;
//        }
//        intervals2 = intervals2.OrderBy(t => t.Item1).ThenBy(t => t.Item2).ToArray();
//        i--;
//    }
//}

//for (i = 0; i < intervals2.Length; i++)
//{
//    //    Console.WriteLine(intervals2[i].Item1);
//    //    Console.WriteLine(intervals2[i].Item2);
//    //    Console.WriteLine(intervals2[i]);
//}
//int total = 0;

//for (i = 0; i < intervals2.Length; i++)
//{
//    total += intervals2[i].Item2 - intervals2[i].Item1;
//}

//// Console.WriteLine(total);

////return total;


///----------------------------------------------------------------

//**** Method Below works but times out ****

//(int, int)[] intervals = {

//    //(1, 2), (6, 10), (11, 15)

////(-35, -25),
////(178, 179),
////(422, 436),
////(-358, -349),
////(403, 416),
////(125, 131),
////(462, 479),
////(305, 319),
////(-338, -328),
////(248, 257),
////(-378, -359),
////(-234, -214),
////(164, 170),
////(-368, -348)

////expected 150

////was 159
//    };

//int[] myArray = { -1000000001 };

//int i = 0;
//int j = 0;
//int k = 0;

//int a = 0;
//int b = 0;

//bool alreadyInArray = false;

//int finalNumber = 0;

//for (i = 0; i < intervals.Length; i++)
//{
//    if (intervals[i].Item1 != intervals[i].Item2) // Making sure a is not equal to b
//    {
//        a = intervals[i].Item1;
//        b = intervals[i].Item2;

//        for (j = a; j < b; j++) // From a to b
//        {
//            for (k = 0; k < myArray.Length; k++) //Iterate through each index of myArray to make sure no duplicates
//            {
//                if (myArray[k] == j) //Case where already in array, don't add again
//                {
//                    alreadyInArray = true;
//                    break;
//                }
//            }
//            if (alreadyInArray == false) // Case where not yet in array. Want to add to array then.
//            {
//                myArray = myArray.Append(j).ToArray(); //adding to end of the array
//            }
//            alreadyInArray = false; //reset back to false to get ready for next iteration of loop
//        }
//    }
//}

//for (i = 1; i < myArray.Length; ++i) //Note: we do not want to count entry in index 0 as that was just inserted to initialize the array
//{
//    finalNumber++;
//}

//Console.WriteLine(finalNumber);

//return finalNumber;

//-----------------------------------------------------------------------------------------------------------------------------