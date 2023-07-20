namespace MoogleEngine;
//En esta clase se va a implementar el funcionamiento de las recomendaciones de Moogle

public class Recomendations
{
    //Método para recomendar la palabra de la lista más parecida a una palabra X
    public static string Recommend(string a, Word[]b)
    {
        static int LevenshteinDistance(string a, string b)  //Método para calcular la distancia de Levenshtein
        {
            var aLenght = a.Length;
            var bLenght = b.Length;
            var matrix = new int [aLenght+1,bLenght+1];
            if(aLenght==0)
            {
                return bLenght;
            }
            if(bLenght==0)
            {
                return aLenght;
            }
            for(var i = 0; i<=aLenght; matrix[i,0] = i++){}
            for(var j = 0; j<=bLenght; matrix[0,j] = j++){}
            for(var i = 1; i<=aLenght; i++)
            {
                for(var j = 1; j<=bLenght; j++)
                {
                    var cost = (b[j-1]==a[i-1]) ? 0:1;
                    matrix[i,j] = Math.Min(Math.Min(matrix[i-1,j]+1,matrix[i,j-1]+1),matrix[i-1,j-1]+cost);
                }
            }
            return matrix[aLenght,bLenght];
        }
        foreach(Word x in b)
        {
            if(a==x.Base)
            {
                return a;
            }
        }    
        int min = LevenshteinDistance(a,b[0].Base);
        string record = b[0].Base;
        for(int i = 1; i<b.Length; i++)
        {
            if(min>LevenshteinDistance(a,b[i].Base))
            {
                min = LevenshteinDistance(a,b[i].Base);
                record = b[i].Base;
            }
        }
        return record;
    }

    //Método para agregar la palabra parecida a la lista de palabras
    public static string[] AgregateRecom(string a, string[]b)
    {
        string[] S = new string[b.Length+1];
        for(int i = 0; i<b.Length; i++)
        {
            S[i] = b[i];
        }
        S[S.Length-1] = a;
        return S;
    }

    //Método para convertir la lista de recomendaciones a un solo string
    public static string StringRecom(string[]a)
    {
        string S = String.Join(" ",a);
        return S;
    }
}