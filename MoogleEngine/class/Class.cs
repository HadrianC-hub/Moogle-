namespace MoogleEngine;
//Aquí guardo todas las clases de elementos que voy a crear para trabajar

public class File   //En esta clase voy a crear elementos de tipo File(Archivos) para trabajar con los documentos de búsqueda
{
    public string Name;    //Dichos elementos contendrán un nombre, que será lo primero que se muestre en la búsqueda
    public string Words;   //Y también contendrán palabras.
    public File (FileInfo x)       //Este es el constructor de este tipo de dato
    {
        this.Name = Functions.NameFile(x.Name);         //Definiendo el nombre del documento
        StreamReader St = new StreamReader(x.FullName); 
        this.Words = St.ReadToEnd();                    //Definiendo el texto del documento
    }                                                   
}

public class Word  //Esta clase va a contener a todas las palabras dentro de todos los Files y a su valor del score
{
    public string Base;    //Esto representa a la palabra en sí como cadena de caracteres
    public float[] score;  //Esta es la cadena de score que la palabra tiene dentro de cada documento
                          
    public Word (string a, File[] b)  //Este es el constructor de este tipo de dato
    {
        this.Base=a;                                //Nombre de la palabra
        this.score = Functions.GetScore(a,b);       //Obtención del score
    }                          
}

public class LINE   //Esta clase va a contener las líneas de respuestas de la búsqueda
{   
    //Una línea de respuesta no es más que un tipo de dato que contiene toda la información necesaria para una respuesta de
    //tipo SearchItem. Básicamente es el puente entre la comparación de listas de palabras y archivos y la respuesta final
    public string Name;     //El nombre de la palabra
    public string Doc;      //El título del documento
    public float Score;     //El score de la plabra en el documento
    public string Snippet;  //El snippet de la palabra en el documento
    public string Text;
    public LINE (string a, string b, float c, string d, string e)     //Este es el constructor de este tipo de dato
    {
        this.Name = a;
        this.Doc = b;
        this.Score = c;
        this.Snippet = d;
        this.Text = e;
    }
}