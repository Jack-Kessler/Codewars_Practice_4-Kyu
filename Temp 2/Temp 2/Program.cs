using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

long n = 51226262651257;
long nCopy = n;
long result = 0;

int duplicateCounter = 0;
int zeroCounter = 0;
int notZeroIndex = 0;
int numberOfDuplicates = 0;
int i = 0;
int j = 0;
int x = 0;
int a = -1;

int counter = 1;

bool isLessThan = false;

var resultList = new List<long>();
var tempList = new List<long>();


string str = n.ToString();
//long[] originalOrder = new long[str.Length];


List<long> originalOrder = new List<long>();

while (nCopy > 0)
{
    originalOrder.Add(nCopy % 10);
    nCopy = nCopy / 10;
}
originalOrder.Reverse();

//for (i = 0; i < originalOrder.Count(); i++)
//{
//    originalOrder[i] = (int)Char.GetNumericValue(str[i]);
//}

int originalFirstDigit = (int)originalOrder[0];

for (i = 0; i < originalOrder.Count(); i++)
{
    if (originalOrder[i] == originalFirstDigit) //Counting how many duplicates of the original first digit there are
    {
        duplicateCounter++;
    }
    else if (originalOrder[i] == 0) //Counting how many zeros digits there are
    {
        zeroCounter++;
    }
    if (duplicateCounter == originalOrder.Count()) //Case: all digits are the same (i.e. 555 or 8888)
    {
        result = -1;
    }
}

numberOfDuplicates = duplicateCounter - 1;

if (originalOrder.Count() == 1) //Case 1: Only 1 digit to begin with (i.e. 1 - 9)
{
    result = -1;
}
else if (originalOrder.Count() == 2) //Case 2: Only 2 digits to begin with (i.e. 10 - 99)
{
    if (originalOrder[1] == 0)
    {
        result = -1;
    }
    else if (originalOrder[1] >= originalOrder[0])
    {
        result = -1;
    }
    else
    {
        resultList.Add(originalOrder[1]);
        resultList.Add(originalOrder[0]);
        foreach (long p in resultList)
        {
            result = 10 * result + p;
        }
    }
}
else if (originalOrder.Count() == 3) //Case 3: Three or more digits originally (i.e. 100 ~ )
{
    if (zeroCounter == 2) //Case: all but original first digit are zeros.
    {                                       //Remember - Assuming original first digit not equal to zero.
        result = -1;
    }
    else if (zeroCounter == 1) //Case: one zero and two non-zero digits
    {
        if (numberOfDuplicates == 0) //Case: two non-zero digits are NOT equal
        {
            if (originalOrder[1] == 0) //Ex. 605 or 607
            {
                if (originalOrder[0] < originalOrder[2]) //Ex. 607
                {
                    result = -1;
                }
                else // Ex. 605
                {
                    resultList.Add(originalOrder[2]);
                    resultList.Add(originalOrder[0]);
                    resultList.Add(originalOrder[1]);
                    foreach (long p in resultList)
                    {
                        result = 10 * result + p;
                    }
                }
            }
            else if (originalOrder[2] == 0) //Ex. 670 ---> 607 OR 650 ---> 605
            {
                resultList.Add(originalOrder[0]);
                resultList.Add(originalOrder[2]);
                resultList.Add(originalOrder[1]);
                foreach (long p in resultList)
                {
                    result = 10 * result + p;
                }
            }
            else
            {
                Console.WriteLine("Error #1: This should never happen");
            }
        }
        else //Case: one zero and two non-zero digits; two non-zero digits are equal (ex. 550 & 505)
        {
            if (originalOrder[0] == originalOrder[1]) //ex. 550
            {
                resultList.Add(originalOrder[0]);
                resultList.Add(originalOrder[2]);
                resultList.Add(originalOrder[1]);
                foreach (long p in resultList)
                {
                    result = 10 * result + p;
                }
            }
            else //ex. 505
            {
                result = -1;
            }
        }
    }
    else if (zeroCounter == 0) //Case: No zeros (ex.521, 512, 516, 567, 544, 566 Duplicates w/ first digit: 559, 551, 595, 545)
    {
        if (numberOfDuplicates == 1) //First, tackle cases with duplicate of first digit. Ex. 559, 551, 595, 545
        {
            if (originalOrder[0] == originalOrder[1]) //Ex. 559, 551
            {
                if (originalOrder[0] > originalOrder[2]) //Ex. 551 ---> 515
                {
                    resultList.Add(originalOrder[0]);
                    resultList.Add(originalOrder[2]);
                    resultList.Add(originalOrder[1]);
                    foreach (long p in resultList)
                    {
                        result = 10 * result + p;
                    }
                }
                else // Ex. 559
                {
                    result = -1;
                }
            }
            else if (originalOrder[0] == originalOrder[2]) //Ex.  595, 515
            {
                if (originalOrder[0] < originalOrder[1]) //Ex. 595 ---> 559
                {
                    resultList.Add(originalOrder[0]);
                    resultList.Add(originalOrder[2]);
                    resultList.Add(originalOrder[1]);
                    foreach (long p in resultList)
                    {
                        result = 10 * result + p;
                    }
                }
                else //Ex. 515 ---> 155
                {
                    resultList.Add(originalOrder[1]);
                    resultList.Add(originalOrder[0]);
                    resultList.Add(originalOrder[2]);
                    foreach (long p in resultList)
                    {
                        result = 10 * result + p;
                    }
                }
            }
        }
        else if (originalOrder[0] != originalOrder[1] && originalOrder[1] == originalOrder[2])
        //No zeros. No first digit duplicates. Other two digits are duplicates. Ex. 544, 566, 454, 656
        {

            if (originalOrder[0] > originalOrder[1]) //Ex. 544 ---> 454
            {
                resultList.Add(originalOrder[1]);
                resultList.Add(originalOrder[0]);
                resultList.Add(originalOrder[2]);
                foreach (long p in resultList)
                {
                    result = 10 * result + p;
                }
            }
            else // Ex. 566
            {
                result = -1;
            }
        }
        else if (originalOrder[0] != originalOrder[1] && originalOrder[0] == originalOrder[2])
        //No zeros. No first digit duplicates. Other two digits are duplicates
        {
            if (originalOrder[0] < originalOrder[1]) //Ex. 454 ---> 445
            {
                resultList.Add(originalOrder[0]);
                resultList.Add(originalOrder[2]);
                resultList.Add(originalOrder[1]);
                foreach (long p in resultList)
                {
                    result = 10 * result + p;
                }
            }
            else // Ex. 656 ---> 566
            {
                resultList.Add(originalOrder[1]);
                resultList.Add(originalOrder[0]);
                resultList.Add(originalOrder[2]);
                foreach (long p in resultList)
                {
                    result = 10 * result + p;
                }
            }
        }
        else if (numberOfDuplicates == 0 && originalOrder[0] != originalOrder[1] &&
                    originalOrder[0] != originalOrder[2] && originalOrder[1] != originalOrder[2])
        //No Zeros. No duplicates at all. All three digits are different. Ex. 531, 513, 135
        {
            if (originalOrder[2] < originalOrder[1]) //Ex. 531  --> 513
            {
                resultList.Add(originalOrder[0]);
                resultList.Add(originalOrder[2]);
                resultList.Add(originalOrder[1]);
                foreach (long p in resultList)
                {
                    result = 10 * result + p;
                }
            }
            else if (originalOrder[0] > originalOrder[1] && originalOrder[2] > originalOrder[1] && originalOrder[0] > originalOrder[2]) //Ex. 513 ---> 351
                                                                                                                                        //(Note: 531 case was taken care of above)
            {
                resultList.Add(originalOrder[2]);
                resultList.Add(originalOrder[0]);
                resultList.Add(originalOrder[1]);
                foreach (long p in resultList)
                {
                    result = 10 * result + p;
                }
            }
            else if (originalOrder[0] > originalOrder[1] && originalOrder[2] > originalOrder[1] && originalOrder[2] > originalOrder[0]) //Ex. 315 ---> 351
                                                                                                                                        //(Note: 531 case was taken care of above)
            {
                resultList.Add(originalOrder[1]);
                resultList.Add(originalOrder[2]);
                resultList.Add(originalOrder[0]);
                foreach (long p in resultList)
                {
                    result = 10 * result + p;
                }
            }
            else if (originalOrder[2] > originalOrder[1]) //Ex. 135
            {
                result = -1;
            }
            else
            {
                Console.WriteLine("Error #2: Should never happen.");
            }
        }
        else
        {
            Console.WriteLine("Error #3: Should never happen.");
        }
    }
    else
    {
        Console.WriteLine("Error #4: Should never happen");
    }
}
else //*** 4 DIGITS OR MORE ***
{
    if (zeroCounter == originalOrder.Count() - 1) // Case 1: All but 1 digit (original first digit) are zeros. Ex. 500000 or 70000000000000
    {
        result = -1;
    }
    else if (originalOrder[originalOrder.Count() - 1] < originalOrder[originalOrder.Count() - 2]) //Case 2: Can just flip last two digits
    {
        for (i = 0; i < originalOrder.Count() - 2; i++)
        {
            resultList.Add(originalOrder[i]); //Create duplicate list of original order array
        }
        for (i = 0; i < 1; i++)
        {
            resultList.Add(originalOrder[originalOrder.Count() - 1]);
        }
        for (i = 0; i < 1; i++)
        {
            resultList.Add(originalOrder[originalOrder.Count() - 2]);
        }
        foreach (long p in resultList)
        {
            result = 10 * result + p;
        }
    }
    else if (zeroCounter == originalOrder.Count() - 2) // Case 3: All but 2 digits (original first digit + one more) are zeros.
    {
        if (originalOrder[originalOrder.Count() - 1] != 0) //Ex. 50005, 500006, 500004
        {
            if (originalOrder[originalOrder.Count() - 1] < originalOrder[0]) //Ex. 500004 & 500001
            {
                for (i = 0; i < originalOrder.Count(); i++)
                {
                    if (i == 0)
                    {
                        resultList.Add(originalOrder[originalOrder.Count() - 1]);
                    }
                    else if (i == 1)
                    {
                        resultList.Add(originalOrder[0]);
                    }
                    else
                    {
                        resultList.Add(0);
                    }
                }
                foreach (long p in resultList)
                {
                    result = 10 * result + p;
                }
            }
            else //Ex. 500005 & 500007
            {
                result = -1;
            }
        }
        else
        {
            for (i = 1; i < originalOrder.Count(); i++) //Assuming original first digit is non-zero. Ex. 500500 or 507000
            {
                if ((originalOrder[i] != 0))
                {
                    notZeroIndex = i;       //i is index of non-zero digit that is not the original first digit
                    break;
                }
            }
            resultList.Add(originalOrder[0]); //have to first add first digit into resultList.
            for (i = 1; i < originalOrder.Count(); i++)
            {
                if (i == notZeroIndex)
                {
                    resultList.Add(originalOrder[i + 1]);
                }
                else if (i == notZeroIndex + 1)
                {
                    resultList.Add(originalOrder[i - 1]);
                }
                else
                {
                    resultList.Add(originalOrder[i]);
                }
            }
            foreach (long p in resultList)
            {
                result = 10 * result + p;
            }
        }
    }
    else
    {
        if (originalOrder[1] == 0) //Zero is in index 1 spot.
        {
            tempList.Clear();
            for (i = 2; i < originalOrder.Count(); i++)
            {
                if (originalOrder[i] < originalOrder[0] && originalOrder[i] != 0)
                {
                    isLessThan = true;
                }
                if (isLessThan == false) //Every number to right of zero in index 1 spot is greater than or equal to first digit.
                {
                    result = -1;
                    break;
                }
            }
            
        }
        else
        {
            // Gerenic method of starting from the right, checking back
            tempList.Clear();
            for (i = originalOrder.Count() - 2; i >= 1; i--)
            {
                if (originalOrder[i] < originalOrder[i - 1])
                {
                    for (j = i + 1; j < originalOrder.Count(); j++)
                    {
                        tempList.Add(originalOrder[j]); //adds all digits to right of swap point.
                                                        //Note: we already know last digit is greater than second to last digit from logic above.
                    }
                    tempList.Sort();
                    for (j = 0; j < tempList.Count(); j++)
                    {
                        if (tempList[j] < originalOrder[i - 1] && tempList[j] > a) //Note: tempList cannot have 0 as an element
                        {
                            a = (int)tempList[j];
                        }
                    }
                    if (a != -1)
                    {
                        for (j = i + 1; j < originalOrder.Count(); j++)
                        {
                            if (originalOrder[j] == a) //Note: tempList cannot have 0 as an element
                            {
                                break;      //Finding index (j) in originalOrder where value = a.
                            }
                        }
                        tempList.Clear();
                        for (x = originalOrder.Count() - 1; x > j; x--)
                        {
                            tempList.Add(originalOrder[x]); //Add everything to right of j
                        }
                        for (x = i - 1; x < j; x++)
                        {
                            tempList.Add(originalOrder[x]); //add everything to the left of j including digit at i-1 position.
                        }
                        tempList.Sort();
                        tempList.Reverse();
                        resultList.Clear();
                        if (i - 1 == 0)
                        {
                            resultList.Add(originalOrder[j]);
                            for (x = 0; x < tempList.Count(); x++) //Last, add digits in temp list from greatest to least
                            {
                                resultList.Add(tempList[x]);
                            }
                            foreach (long p in resultList)
                            {
                                result = 10 * result + p;
                            }
                        }
                        else
                        {
                            for (x = 0; x < i - 1; x++)
                            {
                                resultList.Add(originalOrder[x]); //Start building result list. First add everything to left of i - 1.
                            }
                            resultList.Add(originalOrder[j]); //Next, swap out next largest digit after i -1 in i - 1 position.
                            for (x = 0; x < tempList.Count(); x++) //Last, add digits in temp list from greatest to least
                            {
                                resultList.Add(tempList[x]);
                            }
                            foreach (long p in resultList)
                            {
                                result = 10 * result + p;
                            }
                            break;
                        }
                    }
                    else // case where no other digit to the right of i is less than digit to the left of i.
                            // In this case, need to slide i one to the left (in i - 1 position) and reorder everything to the right, greatest to least
                    {
                        tempList.Add(originalOrder[i - 1]);
                        tempList.Sort();
                        tempList.Reverse();
                        for (j = 0; j < i - 1; j++)
                        {
                            resultList.Add(originalOrder[j]); //Adds up to swap point
                        }
                            resultList.Add(originalOrder[i]); //Next add i
                        for (j = 0; j < tempList.Count(); j++) //Lastly, add templist
                        {
                            resultList.Add(tempList[j]);
                        }
                        foreach (long p in resultList)
                        {
                            result = 10 * result + p;
                        }
                        break;
                    }

                }
                else
                {
                    counter++;    
                    if (counter > originalOrder.Count() -2)
                    {
                        result = -1;
                        break;
                    }
                }
            }
        }
    }
}
Console.WriteLine(result);


            //    tempList.Clear();
            //    for (x = originalOrder.Length - 1; x > i; x--)
            //    {
            //        tempList.Add(originalOrder[x]); //templist has everything to right of zero.
            //    }
            //    for (x = j - 1; x >= 0; x--)
            //    {
            //        if (originalOrder[x] != 0) // x is the same as non-zero index. This conditional must be true.
            //        {
            //            break;
            //        }
            //    }
            //    for (int y = x; y < j; y++) //Add digits between zero index and nearest non-zero digit to left including that digit.
            //                                //Remember: j is index for zero. x is nearest non-zero to the left.
            //    {
            //        tempList.Add(originalOrder[y]);
            //    }
            //    tempList.Sort(); //Sorts list from smallest to biggest
            //    tempList.Reverse(); // Now sorted from biggest to smallest
            //    for (int y = 0; y < x; y++)
            //    {
            //        resultList.Add(originalOrder[y]); //Add up to closest non-zero digit to zero
            //    }
            //    for (int y = 0; y < 1; y++)
            //    {
            //        resultList.Add(originalOrder[j]); //Add zero
            //    }
            //    for (int y = 0; y < tempList.Count(); y++) // Remember: tempList is sorted greatest to smallest
            //    {
            //        resultList.Add(tempList[y]);
            //    }
            //    foreach (long p in resultList)
            //    {
            //        result = 10 * result + p;
            //    }
            //    done = true;
            //    break;


            //else if (originalOrder[j - counter] == 0)
            //    {
            //        break;
            //    }
            //    else if (originalOrder[j - counter] > originalOrder[j])
            //    {
            //        for (x = 0; x < tempList.Count(); x++)
            //        {
            //            if (tempList[x] != 0 && tempList[x] > a && tempList[x] < originalOrder[j - counter + 1])
            //            {
            //                a = tempList[x];
            //            }
            //        }
            //        if (a != -1)
            //        {
            //            for (x = 0; x < tempList.Count(); x++)
            //            {
            //                if (tempList[x] == a)
            //                {
            //                    break;
            //                }
            //            }
            //            tempList.Clear();

            //            for (int y = originalOrder.Length - 1; y > originalOrder.Length - 1 - x; y--)
            //            {
            //                tempList.Add(originalOrder[y]);
            //            }
            //            for (int y = originalOrder.Length - 1 - x - 1; y > j - counter; y--)
            //            {
            //                tempList.Add(originalOrder[y]);
            //            }
            //            tempList.Sort();
            //            tempList.Reverse();
            //            //Now a is largest digit less than digit at (j - counter +1)
            //            for (int y = 0; y < j - counter + 1; y++)
            //            {
            //                resultList.Add(originalOrder[y]); //Adds up to flip point
            //            }
            //            for (int y = j - counter + 1; y < j - counter + 2; y++)
            //            {
            //                resultList.Add(originalOrder[a]);
            //            }
            //            for (int y = 0; y < tempList.Count(); y++) // Remember: tempList is sorted greatest to smallest
            //            {
            //                resultList.Add(tempList[y]); //Adds rest of digits sorted greatest to smallest
            //            }
            //            foreach (long p in resultList)
            //            {
            //                result = 10 * result + p;
            //            }
            //            done = true;
            //            break;
            //        }
            //        tempList.RemoveAt(counter2 - 1); //Remove to avoid double add
            //        tempList.Add(originalOrder[j - counter]); // Add so we can sort.
            //        tempList.Sort(); //Sorts list from smallest to biggest
            //        tempList.Reverse(); // Now sorted from biggest to smallest
            //        for (x = 0; x < tempList.Count; x++)
            //        {
            //            // Console.WriteLine(tempList[x]);
            //        }

            //        for (x = 0; x < j - counter; x++)
            //        {
            //            resultList.Add(originalOrder[x]); //Adds up to flip point
            //        }
            //        for (x = j - counter; x < j - counter + 1; x++)
            //        {
            //            resultList.Add(originalOrder[j]); //Adds flip digit
            //        }
            //        for (x = 0; x < tempList.Count(); x++) // Remember: tempList is sorted greatest to smallest
            //        {
            //            resultList.Add(tempList[x]); //Adds rest of digits sorted greatest to smallest
            //        }
            //        foreach (long p in resultList)
            //        {
            //            result = 10 * result + p;
            //        }
            //        done = true;
            //        break;
            //    }
            //    else
            //    {
            //        if (counter2 == 1 && counter == 1)
            //        {
            //            tempList.Add(originalOrder[originalOrder.Length - 1]);
            //        }
            //        tempList.Add(originalOrder[j - counter]);
            //        counter++;
            //    }
            //}
            //counter = 1;
            //counter2++;
            //tempList.Clear();
            //for (x = originalOrder.Length - 1; x > originalOrder.Length - 1 - counter2; x--)
            //{
            //    tempList.Add(originalOrder[x]);
            //}
            //if (done == true)
            //{
            //    break;
            //}




//    else // **** At least 3 non-zero digits. Remember, there are at least 4 digits.
//         // There may be anywhere from 0 to (total # of digits - 2 zeros). *****
//    {
//        for (i = 0; i < originalOrder.Length; i++)
//        {
//            resultList.Add(originalOrder[i]); //Create duplicate list of original order array
//        }
//        resultList.Sort();

//        notZeroIndex = originalOrder.Length - (originalOrder.Length - zeroCounter); // confirmed correct

//        if (resultList[notZeroIndex] == originalFirstDigit) //**** Case: original first digit was smallest non-zero digit *****
//        {
//            if (numberOfDuplicates == originalOrder.Length - 2) //Case: all digits except one are duplicates. Ex. 55550 or 50555
//            {
//                if (originalOrder[1] == 0) //Ex. 50555
//                {
//                    result = -1;
//                }
//                else //Ex. 55550 or 55505
//                {
//                    resultList.Clear();
//                    for (j = 0; j < originalOrder.Length; j++)
//                    {
//                        if (originalOrder[j] == 0)      // j is zero index
//                        {
//                            break;
//                        }
//                    }
//                    for (x = 0; x < originalOrder.Length; x++)
//                    {
//                        if (x == j)
//                        {
//                            resultList.Add(originalOrder[j - 1]);
//                        }
//                        else if (x == j - 1)
//                        {
//                            resultList.Add(originalOrder[j]);
//                        }
//                        else
//                        {
//                            resultList.Add(originalOrder[x]);
//                        }
//                    }
//                    foreach (long p in resultList)
//                    {
//                        result = 10 * result + p;
//                    }
//                }
//            }
//            else //Number of digits is at least 4
//                 //Original first digit was smallest non-zero digit
//                 //At least 3 non - zero digits that are not the same
//                 //Number of zeros range from 0 to (total number of digits - 2)
//                 //Ex. 50765 (sorted: <05567>) or 58765 (sorted: <55678>) or 50075 (sorted: 00557) etc...
//            {
//                indexOneValue = originalOrder[1];
//                if (originalOrder[1] == 0) //Ex. 50765
//                {
//                    result = -1;
//                }
//                else if (originalOrder[originalOrder.Length - 2] == 0 &&
//                         originalOrder[originalOrder.Length - 1] != 0 &&
//                         originalOrder[originalOrder.Length - 3] > originalOrder[originalOrder.Length - 1])
//                {
//                    resultList.Clear();
//                    for (j = 0; j < originalOrder.Length - 3; j++)
//                    {
//                        resultList.Add(originalOrder[j]); //Create duplicate list of original order array
//                    }
//                    for (j = 0; j < 1; j++)
//                    {
//                        resultList.Add(originalOrder[originalOrder.Length - 1]);
//                    }
//                    for (j = 0; j < 1; j++)
//                    {
//                        resultList.Add(originalOrder[originalOrder.Length - 3]);
//                    }
//                    for (j = 0; j < 1; j++)
//                    {
//                        resultList.Add(originalOrder[originalOrder.Length - 2]);
//                    }
//                    foreach (long p in resultList)
//                    {
//                        result = 10 * result + p;
//                    }
//                }
//                else // digit next to first digit has to be equal to or greater than first digit.
//                     // any zeros cannot be in first two positions to left
//                     // Ex. 567085 or 556708 (note: 8 > 7) or last position (due to logic above)
//                {
//                    resultList.Clear();
//                    for (i = originalOrder.Length - 1; i >= 0; i--)
//                    {
//                        if (counter2 == originalOrder.Length - 1)
//                        {
//                            result = -1; //Case: 5567789
//                            break;
//                        }
//                        else
//                        {
//                            while (counter <= originalOrder.Length - counter2)
//                            {
//                                if (originalOrder[i] == 0)
//                                {
//                                    tempList.Clear();
//                                    for (j = originalOrder.Length - 1; j > i; j--)
//                                    {
//                                        tempList.Add(originalOrder[j]); //templist has everything to right of zero.
//                                    }
//                                    for (j = i - 1; j >= 0; j--)
//                                    {
//                                        if (originalOrder[j] != 0) // j is the same as non-zero index. This conditional must be true.
//                                        {
//                                            break;
//                                        }
//                                    }
//                                    for (x = j; x < i; x++) //Need to add digits between zero index and nearest non-zero digit to left.
//                                                            //Remember: i is index for zero. j is nearest non-zero to the left.
//                                    {
//                                        tempList.Add(originalOrder[x]);
//                                    }
//                                    tempList.Sort(); //Sorts list from smallest to biggest
//                                    tempList.Reverse(); // Now sorted from biggest to smallest

//                                    for (x = 0; x < j; x++)
//                                    {
//                                        resultList.Add(originalOrder[x]); //Add up to closest non-zero digit to zero
//                                    }
//                                    for (x = 0; x < 1; x++)
//                                    {
//                                        resultList.Add(originalOrder[i]); //Add zero
//                                    }
//                                    for (x = 0; x < tempList.Count(); x++) // Remember: tempList is sorted greatest to smallest
//                                    {
//                                        resultList.Add(tempList[x]);
//                                    }
//                                    foreach (long p in resultList)
//                                    {
//                                        result = 10 * result + p;
//                                    }
//                                    done = true;
//                                    break;
//                                }
//                                else if (originalOrder[i - counter] == 0)
//                                {
//                                    break;
//                                }
//                                else if (originalOrder[i - counter] > originalOrder[i])
//                                {
//                                    if (originalOrder[i - counter] == originalOrder[originalOrder.Length - 2] &&
//                                        originalOrder[i] == originalOrder[originalOrder.Length - 1])
//                                    {
//                                        for (x = 0; x < i - counter; x++)
//                                        {
//                                            resultList.Add(originalOrder[x]); //Adds up to flip point
//                                        }
//                                        for (x = 0; x < 1; x++)
//                                        {
//                                            resultList.Add(originalOrder[i]); //Adds flip digit
//                                        }
//                                        for (x = 0; x < 1; x++) // Remember: tempList is sorted greatest to smallest
//                                        {
//                                            resultList.Add(originalOrder[i - 1]); //Adds rest of digits sorted greatest to smallest
//                                        }
//                                        foreach (long p in resultList)
//                                        {
//                                            result = 10 * result + p;
//                                        }
//                                        done = true;
//                                        break;
//                                    }
//                                    for (x = 0; x < tempList.Count(); x++)
//                                    {
//                                        if (tempList[x] != 0 && tempList[x] > a && tempList[x] < originalOrder[i - counter + 1])
//                                        {
//                                            a = tempList[x];
//                                        }
//                                    }
//                                    if (a != -1)
//                                    {
//                                        for (x = 0; x < tempList.Count(); x++)
//                                        {
//                                            if (tempList[x] == a)
//                                            {
//                                                break;
//                                            }
//                                        }
//                                        tempList.Clear();

//                                        for (int y = originalOrder.Length - 1; y > originalOrder.Length - 1 - x; y--)
//                                        {
//                                            tempList.Add(originalOrder[y]);
//                                        }
//                                        for (int y = originalOrder.Length - 1 - x - 1; y > i - counter; y--)
//                                        {
//                                            tempList.Add(originalOrder[y]);
//                                        }
//                                        tempList.Sort();
//                                        tempList.Reverse();
//                                        //Now a is largest digit less than digit at (j - counter +1)
//                                        for (int y = 0; y < i - counter + 1; y++)
//                                        {
//                                            resultList.Add(originalOrder[y]); //Adds up to flip point
//                                        }
//                                        for (int y = i - counter + 1; y < i - counter + 2; y++)
//                                        {
//                                            resultList.Add(originalOrder[a]);
//                                        }
//                                        for (int y = 0; y < tempList.Count(); y++) // Remember: tempList is sorted greatest to smallest
//                                        {
//                                            resultList.Add(tempList[y]); //Adds rest of digits sorted greatest to smallest
//                                        }
//                                        foreach (long p in resultList)
//                                        {
//                                            result = 10 * result + p;
//                                        }
//                                        done = true;
//                                        break;
//                                    }
//                                    else
//                                    {
//                                        tempList.RemoveAt(counter2 - 1); //Remove to avoid double add
//                                        tempList.Add(originalOrder[i - counter]); // Add so we can sort.
//                                        tempList.Sort(); //Sorts list from smallest to biggest
//                                        tempList.Reverse(); // Now sorted from biggest to smallest

//                                        for (j = 0; j < i - counter; j++)
//                                        {
//                                            resultList.Add(originalOrder[j]);
//                                        }
//                                        for (j = i - counter; j < i - counter + 1; j++)
//                                        {
//                                            resultList.Add(originalOrder[i]);
//                                        }
//                                        for (j = 0; j < tempList.Count(); j++) // Remember: tempList is sorted greatest to smallest
//                                        {
//                                            resultList.Add(tempList[j]);
//                                        }
//                                        foreach (long p in resultList)
//                                        {
//                                            result = 10 * result + p;
//                                        }
//                                        done = true;
//                                        break;
//                                    }
//                                }
//                                else
//                                {
//                                    if (counter2 == 1 && counter == 1)
//                                    {
//                                        tempList.Add(originalOrder[originalOrder.Length - 1]);
//                                    }
//                                    tempList.Add(originalOrder[i - counter]);
//                                    counter++;
//                                }
//                            }
//                            counter = 1;
//                            counter2++;
//                            tempList.Clear();
//                            for (j = originalOrder.Length - 1; j > originalOrder.Length - 1 - counter2; j--)
//                            {
//                                tempList.Add(originalOrder[j]);
//                            }
//                            if (done == true)
//                            {
//                                break;
//                            }
//                        }
//                    }
//                }
//            }
//        }
//        else //Case where original first digit is not smallest digit (excluding zeros)
//             //Ex. 55403 or 57403 or 54703
//             //Note: There does not need to be any zeros either.
//             //Remember: There must be at least 3 non-zero numbers
//        {
//            resultList.Clear();
//            for (j = originalOrder.Length - 1; j >= 0; j--)
//            {
//                if (counter2 == originalOrder.Length - 1)
//                {
//                    if (originalOrder[0] > originalOrder[originalOrder.Length - 1]) //case where first digit is only digit greater than last digit
//                    {
//                        tempList.Clear();
//                        for (x = 0; x < originalOrder.Length - 1; x++)
//                        {
//                            tempList.Add(originalOrder[x]); //Add all but first and last elements of original array
//                        }
//                        tempList.Sort();
//                        tempList.Reverse(); //List is not biggest to smallest
//                        for (x = 0; x < 1; x++)
//                        {
//                            resultList.Add(originalOrder[originalOrder.Length - 1]);
//                        }
//                        for (x = 0; x < tempList.Count(); x++) // Remember: tempList is sorted greatest to smallest
//                        {
//                            resultList.Add(tempList[x]);
//                        }
//                        foreach (long p in resultList)
//                        {
//                            result = 10 * result + p;
//                        }
//                        break;
//                    }
//                    else
//                    {
//                        Console.WriteLine("Error #5: This should never happen.");
//                        break;
//                    }
//                }
//                else
//                {
//                    while (counter <= originalOrder.Length - counter2) // "counter" & "counter2" = 1 originally.
//                    {
//                        if (originalOrder[j] == 0) //j is zero index
//                        {
//                            tempList.Clear();
//                            for (x = originalOrder.Length - 1; x > j; x--)
//                            {
//                                tempList.Add(originalOrder[x]); //templist has everything to right of zero.
//                            }
//                            for (x = j - 1; x >= 0; x--)
//                            {
//                                if (originalOrder[x] != 0) // x is the same as non-zero index. This conditional must be true.
//                                {
//                                    break;
//                                }
//                            }
//                            for (int y = x; y < j; y++) //Add digits between zero index and nearest non-zero digit to left including that digit.
//                                                        //Remember: j is index for zero. x is nearest non-zero to the left.
//                            {
//                                tempList.Add(originalOrder[y]);
//                            }
//                            tempList.Sort(); //Sorts list from smallest to biggest
//                            tempList.Reverse(); // Now sorted from biggest to smallest
//                            for (int y = 0; y < x; y++)
//                            {
//                                resultList.Add(originalOrder[y]); //Add up to closest non-zero digit to zero
//                            }
//                            for (int y = 0; y < 1; y++)
//                            {
//                                resultList.Add(originalOrder[j]); //Add zero
//                            }
//                            for (int y = 0; y < tempList.Count(); y++) // Remember: tempList is sorted greatest to smallest
//                            {
//                                resultList.Add(tempList[y]);
//                            }
//                            foreach (long p in resultList)
//                            {
//                                result = 10 * result + p;
//                            }
//                            done = true;
//                            break;
//                        }
//                        else if (originalOrder[j - counter] == 0)
//                        {
//                            break;
//                        }
//                        else if (originalOrder[j - counter] > originalOrder[j])
//                        {
//                            for (x = 0; x < tempList.Count(); x++)
//                            {
//                                if (tempList[x] != 0 && tempList[x] > a && tempList[x] < originalOrder[j - counter + 1])
//                                {
//                                    a = tempList[x];
//                                }
//                            }
//                            if (a != -1)
//                            {
//                                for (x = 0; x < tempList.Count(); x++)
//                                {
//                                    if (tempList[x] == a)
//                                    {
//                                        break;
//                                    }
//                                }
//                                tempList.Clear();

//                                for (int y = originalOrder.Length - 1; y > originalOrder.Length - 1 - x; y--)
//                                {
//                                    tempList.Add(originalOrder[y]);
//                                }
//                                for (int y = originalOrder.Length - 1 - x - 1; y > j - counter; y--)
//                                {
//                                    tempList.Add(originalOrder[y]);
//                                }
//                                tempList.Sort();
//                                tempList.Reverse();
//                                //Now a is largest digit less than digit at (j - counter +1)
//                                for (int y = 0; y < j - counter + 1; y++)
//                                {
//                                    resultList.Add(originalOrder[y]); //Adds up to flip point
//                                }
//                                for (int y = j - counter + 1; y < j - counter + 2; y++)
//                                {
//                                    resultList.Add(originalOrder[a]);
//                                }
//                                for (int y = 0; y < tempList.Count(); y++) // Remember: tempList is sorted greatest to smallest
//                                {
//                                    resultList.Add(tempList[y]); //Adds rest of digits sorted greatest to smallest
//                                }
//                                foreach (long p in resultList)
//                                {
//                                    result = 10 * result + p;
//                                }
//                                done = true;
//                                break;
//                            }
//                            tempList.RemoveAt(counter2 - 1); //Remove to avoid double add
//                            tempList.Add(originalOrder[j - counter]); // Add so we can sort.
//                            tempList.Sort(); //Sorts list from smallest to biggest
//                            tempList.Reverse(); // Now sorted from biggest to smallest
//                            for (x = 0; x < tempList.Count; x++)
//                            {
//                                // Console.WriteLine(tempList[x]);
//                            }

//                            for (x = 0; x < j - counter; x++)
//                            {
//                                resultList.Add(originalOrder[x]); //Adds up to flip point
//                            }
//                            for (x = j - counter; x < j - counter + 1; x++)
//                            {
//                                resultList.Add(originalOrder[j]); //Adds flip digit
//                            }
//                            for (x = 0; x < tempList.Count(); x++) // Remember: tempList is sorted greatest to smallest
//                            {
//                                resultList.Add(tempList[x]); //Adds rest of digits sorted greatest to smallest
//                            }
//                            foreach (long p in resultList)
//                            {
//                                result = 10 * result + p;
//                            }
//                            done = true;
//                            break;
//                        }
//                        else
//                        {
//                            if (counter2 == 1 && counter == 1)
//                            {
//                                tempList.Add(originalOrder[originalOrder.Length - 1]);
//                            }
//                            tempList.Add(originalOrder[j - counter]);
//                            counter++;
//                        }
//                    }
//                    counter = 1;
//                    counter2++;
//                    tempList.Clear();
//                    for (x = originalOrder.Length - 1; x > originalOrder.Length - 1 - counter2; x--)
//                    {
//                        tempList.Add(originalOrder[x]);
//                    }
//                    if (done == true)
//                    {
//                        break;
//                    }
//                }
//            }
//        }

//    }
//}

//else
//{
//    Console.WriteLine("Error #6: Should never happen.");
//}
//}

//Console.WriteLine(result);
////return result;
