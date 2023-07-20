namespace MoogleEngine;
//En esta clase se va a implementar el trabajo con operadores de búsqueda

public class Operator
{
    //Método para eliminar signos de puntuación y convertir de texto a array de palabras
    public static string[] SimplifyQuery(string a)
    {
        static string[] NullOff(string[] a)
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
        string space = " "; 
        string[] removalmatrix = new string[]{"@","#","`","$","%","&","(",")","_","+","=","-","'",";",":",".",",",">","<","/","?","[","]","{","}"};
        foreach(string c in removalmatrix)
        {
            a=a.Replace(c, space);
        }
        a=a.ToLower(); 
        string[] Simple = a.Split(" "); 
        Simple = NullOff(Simple);
        return Simple;
    }

    //Métodos para agregar palabras con operadores a sus respectivas listas
    public static string[] OpDont(string[]a)
    {
        string[] S = new string[]{""};
        foreach(string x in a)
        {
            if(x[0]=='!')
            {
                string[] T = new string[S.Length+1];
                for(int i=0; i<S.Length; i++)
                {
                    T[i]=S[i];
                }
                T[T.Length-1] = x;
                S = T;
            }
        }
        string[] U = new string[S.Length-1];
        for(int i = 0; i<U.Length; i++)
        {
            U[i] = S[i+1];
        }
        string[] V = new string[U.Length];
        for(int i = 0; i<U.Length; i++)
        {
            char[] m = U[i].ToCharArray();
            char[] n = new char[m.Length-1];
            for(int j = 0; j<n.Length; j++)
            {
                n[j] = m[j+1];
            }
            V[i]=String.Join("",n); 
        }
        return V;
    }
    public static string[] OpHaveTo(string[]a)
    {
        string[] S = new string[]{""};
        foreach(string x in a)
        {
            if(x[0]=='^')
            {
                string[] T = new string[S.Length+1];
                for(int i=0; i<S.Length; i++)
                {
                    T[i]=S[i];
                }
                T[T.Length-1] = x;
                S = T;
            }
        }
        string[] U = new string[S.Length-1];
        for(int i = 0; i<U.Length; i++)
        {
            U[i] = S[i+1];
        }
        string[] V = new string[U.Length];
        for(int i = 0; i<U.Length; i++)
        {
            char[] m = U[i].ToCharArray();
            char[] n = new char[m.Length-1];
            for(int j = 0; j<n.Length; j++)
            {
                n[j] = m[j+1];
            }
            V[i]=String.Join("",n); 
        }
        return V;
    }
    public static string[] OpNearby(string[]a)
    {
        string[] S = new string[]{""};
        foreach(string x in a)
        {
            foreach(char y in x)
            {
                if(y=='~')
                {
                    string[] T = new string[S.Length+1];
                    for(int i=0; i<S.Length; i++)
                {
                    T[i]=S[i];
                }
                T[T.Length-1] = x;
                S = T;
                }
            }
        }
        string[] U = new string[S.Length-1];
        for(int i = 0; i<U.Length; i++)
        {
            U[i] = S[i+1];
        }
        return U;
    }
    public static string[] OpMultiple(string[]a)
    {
        string[] S = new string[]{""};
        foreach(string x in a)
        {
            if(x[0]=='*')
            {
                string[] T = new string[S.Length+1];
                for(int i=0; i<S.Length; i++)
                {
                    T[i]=S[i];
                }
                T[T.Length-1] = x;
                S = T;
                
            }
        }
        string[] U = new string[S.Length-1];
        for(int i = 0; i<U.Length; i++)
        {
            U[i] = S[i+1];
        }
        return U;
    }

    //Métodos para realizar la modificación necesaria en dependencia del operador correspondiente
    public static LINE[] Denegate(LINE[] items, string[] a)
    {
        if(a.Length!=0)
        {
            LINE[] RealItems = new LINE[]{};
            for(int i = 0; i<items.Length; i++)
            {
                foreach(string y in a)
                {
                    if(!items[i].Text.Contains(y)&&!items[i].Doc.Contains(y))
                    {
                        LINE[] RealItemsAdd = new LINE[RealItems.Length+1];
                        for(int j = 0; j<RealItems.Length; j++)
                        {
                            RealItemsAdd[j] = RealItems[j];
                        }
                        RealItemsAdd[RealItemsAdd.Length-1] = items[i];
                        RealItems = RealItemsAdd;
                    }
                }
            }
            return RealItems;
        }
        return items;
    }
    public static LINE[] Admitt(LINE[] items, string[] a)
    {
        if(a.Length!=0)
        {
            LINE[] RealItems = new LINE[]{};
            for(int i = 0; i<items.Length; i++)
            {
                foreach(string y in a)
                {
                    if(items[i].Text.Contains(y)||items[i].Doc.Contains(y))
                    {
                        LINE[] RealItemsAdd = new LINE[RealItems.Length+1];
                        for(int j = 0; j<RealItems.Length; j++)
                        {
                            RealItemsAdd[j] = RealItems[j];
                        }
                        RealItemsAdd[RealItemsAdd.Length-1] = items[i];
                        RealItems = RealItemsAdd;
                    }
                }
            }
            return RealItems;
        }
        return items;
    }
    public static LINE[] Potent(LINE[] items, string[] a)
    {
        if(a.Length!=0)
        {
            foreach(string x in a)
            {
                int count = 1;
                char[] word = x.ToCharArray();
                foreach(char z in word)
                {
                    if(z=='*')
                    {
                        count++;
                    }
                }
                int reversecount = count;
                while(reversecount!=0)
                {
                    char[] mask = new char[word.Length-1];
                    for(int i = 0; i<mask.Length; i++)
                    {
                        mask[i] = word[i+1];
                    }
                    word = mask;
                    reversecount --;                  
                }
                string v = String.Join("",word);
                foreach(LINE y in items)
                {
                    if(y.Text.Contains(v))
                    {
                        y.Score = y.Score*count;
                    }
                }
            }
            return items;
        }
        return items;
    }
    public static LINE[] Nearby(LINE[] items, string[] a)
    {
        if(a.Length!=0)
        {
            LINE[] RealItems = new LINE[items.Length];
            foreach(string x in a)
            {
                string[] words = x.Split('~');
                for(int i = 0; i<RealItems.Length; i++)
                {
                    if(items[i].Text.Contains(words[0])&&items[i].Text.Contains(words[1]))
                    {
                        string[] text = Functions.Simplify(items[i].Text);
                        int[] count1 = new int[]{};
                        int[] count2 = new int[]{};
                        for(int j = 0; j<text.Length; j++)
                        {
                            if(text[i]==words[0])
                            {
                                int[] tempcount = new int[count1.Length+1];
                                for(int k = 0; k<count1.Length; k++)
                                {
                                    tempcount[k] = count1[k];
                                }
                                tempcount[tempcount.Length-1]=j;
                                count1 = tempcount;
                            }
                            if(text[i]==words[1])
                            {
                                int[] tempcount = new int[count2.Length+1];
                                for(int k = 0; k<count2.Length; k++)
                                {
                                    tempcount[k] = count2[k];
                                }
                                tempcount[tempcount.Length-1]=j;
                                count2 = tempcount;
                            }
                        }
                        int distance = int.MaxValue;
                        for(int j = 0; j<count1.Length; j++)
                        {
                            for(int k = 0; k<count2.Length; k++)
                            {
                                if(Math.Max(count1[j],count2[k])-Math.Min(count1[j],count2[k])<distance)
                                {
                                    distance = Math.Max(count1[j],count2[k])-Math.Min(count1[j],count2[k]);
                                }
                            }
                        }
                        items[i].Score = (float)items[i].Score*items.Length*((float)1/(float)distance);
                        RealItems[i] = items[i];
                    }
                    else
                    {
                        RealItems[i] = items[i];
                    }
                }
            }
            return RealItems;
        }
        return items;
    }
    public static LINE[] ModifyAnswerByOperators (LINE[] items, string[] a, string[] b, string[] c, string[] d)
    {
        items = Operator.Denegate(items, a);  
        items = Operator.Admitt(items, b);  
        items = Operator.Potent(items, c);
        items = Operator.Nearby(items, d);
        return items;
    }
}