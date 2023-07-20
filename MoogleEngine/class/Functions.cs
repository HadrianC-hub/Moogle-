namespace MoogleEngine;
//En este archivo guardo todos los métodos que voy a crear para mi trabajo
public class Functions
{   //Método para agregar archivos a la lista vacía (Clase PreProcessedFiles --> Método Process Files)
    public static File[] Add(File[] a, File b)
    {
        File[] S = new File[a.Length+1]; 
        for(int i=0; i<a.Length; i++)    
        {                                
            S[i]=a[i];
        }
        S[a.Length] = b;                 
        return S;
    }

    //Método para establecer el nombre de cada documento (Clase File --> Método File)
    public static string NameFile(string name)
    {
        string[] Split = name.Split(".");
        string RealName = Split[0];
        return RealName;
    }

    //Método para elimnar signos de puntuación de un texto y llevarlo a array de palabras (Clase PreProcessedWords --> Método ProcessWords)
    public static string[] Simplify(string a)
    {
        static string[] NullOff(string[] a)     //Método para eliminar el caracter nulo
        {
            string[] b = new string[] {};
            for(int i = 0; i<a.Length; i++)
            {
                if(a[i]!="")
                {
                    string[]c = new string[b.Length+1];
                    for(int j = 0; j<b.Length; j++)
                    {
                        c[j] = b[j];
                    }
                    c[c.Length-1] = a[i];
                    b=c;
                }
            }
            return b;
        }
        string[] removalmatrix = new string[]{"!","^","*","~","@","#","`","$","%","&","(",")","_","+","=","-","'",";",":",".",",",">","<","/","?","[","]","{","}"};
        foreach(string c in removalmatrix)
        {
            a=a.Replace(c, " ");
        }
        a=a.ToLower();
        string[] Simple = a.Split(" ");
        Simple = NullOff(Simple);
        return Simple;
    }

    //Método para agregar una palabra (Word) a la lista (Clase PreProcessedWords --> Método ProcessWords)
    public static Word[] AddWord(Word[]a , Word b)
    {
        Word[] S = new Word[a.Length+1];
        for(int i=0; i<a.Length; i++)
        {                                
            S[i]=a[i];
        }
        S[a.Length] = b;                 
        return S;
    }

    //Método para remover palabras repetidas de la lista (Clase PreProcessedWords --> Método ProcessWords)
    public static Word[] RemoveDuplicated(Word[]a)
    {
        static bool IsWordHere(Word[]a, Word b)        //Método que determina si una palabra se encuentra en una lista
        {
            foreach(Word x in a)
            {
                if(x.Base==b.Base)
                {
                    return true;
                }
            }
            return false;
        }
        Word[] S = new Word[]{};
        foreach(Word x in a)
        {
            if(!IsWordHere(S,x))
            {
                S = AddWord(S,x);
            }
        }
        return S;
    }

    //Método para hallar el score de una palabra dentro de un conjunto de archivos
    public static float[] GetScore(string a, File[] b)
    {
        float tf;                                           //Declaración de variables
        float[] TF = new float[]{};
        float IDF;
        int wordcount;
        int wordinfile;
        foreach(File x in b)                                //Obtención del TF de la palabra en cada archivo
        {
            wordcount=0;
            string[] realwords = Simplify(x.Words);         
            wordinfile = realwords.Length;
            for(int i = 0; i<wordinfile; i++)
            {
                if(realwords[i]==a)
                {
                    wordcount++;
                }
            }
            tf = ((float)wordcount)/((float)wordinfile);
            TF = Functions.Agregate(TF, tf);                //Obtención del TF de la palabra en todos los archivos
        }
        int fileswithword = 0;                              //Obtención del IDF
        foreach(File x in b)
        {
            string[] realwords = Simplify(x.Words);
            if(IsHere(realwords, a))
            {
                fileswithword++;
            }
        }
        IDF = (float)Math.Log(((float)b.Length)/((float)fileswithword));
        float[] score = new float[]{};                                      //Cálculo TF*IDF
        foreach(float x in TF)
        {
            if(x<=0)
            {
                float individualscore = 0;
                score = Agregate(score, individualscore);
            }
            else
            {
                float individualscore = x * IDF;
                score = Agregate(score, individualscore);
            }
        }
        return score;
    }

    //Método para agregar un TF nuevo a la cadena
    public static float[] Agregate(float[] a, float b)
    {
        float[] S = new float[a.Length+1];
        for(int i=0; i<a.Length; i++)    
        {                                
            S[i]=a[i];
        }
        S[a.Length] = b;                 
        return S;
    }
    
    //Método para determinar si una palabra está contenida en un archivo
    static bool IsHere(string[] a, string b)
    {
        foreach(string x in a)
        {
            if(x==b)
            {
                return true;
            }
        }
        return false;
    }

    //Método para crear una posible línea de respuesta
    public static LINE[] GetResponse(Word a, File[]b)
    {
        LINE[] S = new LINE[b.Length];
        int i = 0;
        foreach(File x in b)
        {
            S[i] = new LINE(a.Base,x.Name,a.score[i],GetSnippet(a,x),x.Words);
            i++;
        }
        return S;

        //Método para obtener el Snippet necesario para la respuesta
        static string GetSnippet(Word a, File b)
        {
            if(IsHere(Simplify(b.Words),a.Base))           //Si la palabra se encuentra en la lista...
            {
                int i = 0;
                foreach(string x in Simplify(b.Words))     //...busco la posición exacta de la palabra en esa lista.
                {
                    if(x!=a.Base)
                    {
                        i++;
                    }
                    else
                    {
                        if(Simplify(b.Words).Length>9)     //Si la lista tiene más de nueve palabras...
                        {
                            if(i>8)                        //...reviso si mi palabra está en el principio de la lista, de no estar ahi...
                            {
                                string[] DividedResponse = b.Words.Split(" ");
                                if(DividedResponse.Length-i>5)  //...reviso si mi palabra está en el final...
                                {
                                    string[] RealDividedResponse = new string[]
                                    {DividedResponse[i-4],DividedResponse[i-3],DividedResponse[i-2],DividedResponse[i-1]
                                    ,DividedResponse[i],DividedResponse[i+1],DividedResponse[i+2],DividedResponse[i+3]
                                    ,DividedResponse[i+4],DividedResponse[i+5]};
                                    return String.Join(' ',RealDividedResponse);    //Suponiendo que esté en el final, devuelve las ultimas palabras
                                }
                                else
                                {
                                    string[] RealDividedResponse = new string[]
                                    {DividedResponse[i-9],DividedResponse[i-8],DividedResponse[i-7],DividedResponse[i-6]
                                    ,DividedResponse[i-5],DividedResponse[i-4],DividedResponse[i-3],DividedResponse[i-2]
                                    ,DividedResponse[i-1],DividedResponse[i]};
                                    return String.Join(' ',RealDividedResponse);   //Suponiendo que esté en el medio, devulve la vecindad correspondiente de longitud 5
                                }
                                
                            }
                            string[] Response = b.Words.Split(" ",10);              
                            string[] RealResponse = new string[Response.Length-1];
                            for(int k = 0; k<RealResponse.Length; k++)
                            {
                                RealResponse[k] = Response[k];
                            }
                            return String.Join(' ',RealResponse);       //Suponiendo que esté en el inicio, devuelve las palabras iniciales del texto
                        }
                        return b.Words;     //Suponiendo que el texto no tiene más de 10 palabras, devuelve el texto completo
                    }
                }
            }
            return "";  //De no aparecer la palabra, devuelve el vacío
        }
    }

    //Método para seleccionar las mejores respuestas de todas las posibles
    public static LINE[] RealAnswer(LINE[]a, File[]b)
    {
        if(a.Length==0)     //De no existir una respuesta posible, devuelve un error
        {   
            return new LINE[]{new LINE("","No se ha encontrado su búsqueda",1.0f,"Intente de nuevo","")};
        }
        LINE[] S = new LINE[b.Length];  //Si existen respuestas, devuelveme las mejores solamente
        int i = 0;
        while(i<b.Length)
        {
            S[i] = a[i];
            i++;
        }
        for(int j = i; j<a.Length; j++)
        {   
            if(i==S.Length)
            {
                i=0;
            }
            
            if(S[i].Score<a[j].Score)
            {
                S[i].Doc = a[j].Doc;
                S[i].Name = a[j].Name;
                S[i].Snippet = a[j].Snippet;
            }
            S[i].Score = S[i].Score+a[j].Score;
            i++;
        }
        return S;
    }

    //Método para convertir las líneas en respuestas de tipo SearchItem
    public static SearchItem[] Output(LINE[]a)
    {
        SearchItem[] S = new SearchItem[a.Length];
        for(int i = 0; i<a.Length; i++)
        {
            S[i] = new SearchItem(a[i].Doc,a[i].Snippet,a[i].Score);
        }
        Array.Sort(S, (x,y)=>y.Score.CompareTo(x.Score));
        int count = 0;
        foreach(SearchItem x in S)
        {
            if(x.Score==0.0000f)
            {
                count++;
            }
        }
        SearchItem[] Output = new SearchItem[S.Length-count];
        for(int i = 0; i<Output.Length; i++)
        {
            Output[i] = S[i];
        }
        return Output;
    }

    //Método para ordenar las respuestas por Score
    public static SearchItem[] OrderByScore(SearchItem[] a)
    {
        SearchItem[] b = a;
        for(int x = 0; x<b.Length; x++)
        {
            for(int i = 0; i<b.Length-x-1; i++)
            {
                if(b[i].Score<a[i+1].Score)
                {
                    SearchItem temp = b[i+1];
                    b[i+1] = b[i];
                    b[i] = temp;
                }
            }
        }
        return b;
    }
}