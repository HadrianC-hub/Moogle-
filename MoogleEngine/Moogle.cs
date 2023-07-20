namespace MoogleEngine;


public static class Moogle
{
    public static SearchResult Query(string query)
        //Los métodos utilizados en esta búsqueda están separados por categorías

        //Los métodos relacionados con la búsqueda en general se encuentran en el apartado Functions
        
        //Los métodos relacionados con el tranajo con operadores se encuentran en el apartado Operators

        //Los métodos relacionados con la creación de recomendaciones para palabras no halladas se encuentra en Recomendations

        //En el apartado Class se encuentran todos los tipos de datos creados para realizar la implementación de la búsqueda

        //En el apartado TextProcessing se realiza todo el procesamiento inicial del texto, a modo de mejorar la funcionalidad
        //y velocidad de la búsqueda

    {   
        //Primeramente se realiza el trabajo de procesamiento de texto en la clase TextProcessing
        //Terminado ese punto, se escogen todas aquellas palabras del query que poseen operadores
        string[] OperatorQuery = Operator.SimplifyQuery(query); //Estas palabras contienen al menos un modificador
        string[] OperatorDont = Operator.OpDont(OperatorQuery); //Estas palabras no deben aparecer en ninguna respuesta
        string[] OperatorHaveTo = Operator.OpHaveTo(OperatorQuery); //Estas palabras tienen que aparecer en cualquier respuesta
        string[] OperatorNearby = Operator.OpNearby(OperatorQuery); //Estas palabras son más importantes mientras más cerca estén
        string[] OperatorMultiple = Operator.OpMultiple(OperatorQuery); //Estas palabras son de mayor importancia

        string[] Search = Functions.Simplify(query);    //Primero convierto mi query en una lista de palabras
        Word[] SearchIn = new Word[]{};                 //Creo una lista vacía que contendrá las palabras del query
        string[] Recomended = new string[]{};         //Creo una lista vacía que contendrá las palabras destinadas a la recomendación
        foreach(string x in Search)                     //Agrego las palabras del query a ambas listas
        {
            foreach(Word y in Processed.Words)
            {
                if(x==y.Base)
                {
                    SearchIn = Functions.AddWord(SearchIn, y);  
                }
            }
            Recomended = Recomendations.AgregateRecom(Recomendations.Recommend(x,Processed.Words),Recomended);
        }
        SearchIn = Functions.RemoveDuplicated(SearchIn); //Esta lista de palabras contiene las halladas en la búsqueda

        //En este punto solo nos queda dar la respuesta, osea, organizar las palabras por score y establecer la prioridad
        //con la que se deben mostrar las búsquedas

        LINE[] Answer = new LINE[]{};           //Declaramos un array de líneas de respuesta vacío
        foreach(Word x in SearchIn)
        {
           Answer = (Answer.Concat(Functions.GetResponse(x,Processed.Files))).ToArray();
        }
        Answer = Functions.RealAnswer(Answer, Processed.Files);//En este punto ya tenemos todas las líneas de respuestas
        Answer = Operator.ModifyAnswerByOperators(Answer,OperatorDont,OperatorHaveTo,OperatorMultiple,OperatorNearby);

        SearchItem[] items = Functions.Output(Answer); //Aquí las convertimos de líneas de respuesta a datos de tipo SearchItem

        items = Functions.OrderByScore(items);  
        string recomendations = Recomendations.StringRecom(Recomended);
        for(int i = 0; i<Math.Min(Functions.Simplify(recomendations).Length,Functions.Simplify(query).Length); i++)
        {
            if(Functions.Simplify(recomendations)[i]!=Functions.Simplify(query)[i])
            {
                return new SearchResult(items, recomendations);
            }
        }
        return new SearchResult(items, "");
    }
}
