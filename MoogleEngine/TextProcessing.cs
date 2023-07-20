namespace MoogleEngine;
//Aquí ocurrirá el procesamiento del texto antes de realizar una búsqueda
public static class PreProcessedFiles             //En esta clase ocurrirá el procesamiento de los archivos
{
    static DirectoryInfo dir = new DirectoryInfo ("..\\Content");  //Directorio de los archivos
    public static FileInfo[] Library = dir.GetFiles("*.txt");      //Obtener todos los archivos con extensión .txt
    public static File[] Files = new File[]{};                     //Crear una lista vacía para insertar en ella mis archivos
    public static File[] ProcessFiles(FileInfo[] Library)          //Método que va a insertar mis archivos en la lista vacía
    {
        foreach(FileInfo x in Library)          
        {                                       
            File y = new File(x);
            Files = Functions.Add(Files, y);
        }
        return Files;
    }
}
public static class PreprocessedWords             //En esta clase ocurrirá el procesamiento de las palabras
{
    public static Word[] Words = new Word[]{};                     //Crear una lista vacía para insertar en ella mis palabras
    public static Word[] ProcessWords()                            //Método que va a insertar mis palabras en la lista vacía
    {
        foreach(File x in Processed.Files)                          
        {
            foreach(string y in Functions.Simplify(x.Words))
            {
                Word z = new Word(y, Processed.Files);
                Words = Functions.AddWord(Words, z);
            }
        }
        Words = Functions.RemoveDuplicated(Words);
        return Words;
    }
}
public static class Processed                     //En esta clase se guardarán la lista estática de archivos y palabras               
{
    public static File[] Files = PreProcessedFiles.ProcessFiles(PreProcessedFiles.Library);     //Archivos
    public static Word[] Words = PreprocessedWords.ProcessWords();                              //Palabras
}