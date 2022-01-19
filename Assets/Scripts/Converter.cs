using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Converter 
{
    public string InsertTable(string tablename, string[] values)
    {
        string query = "insert into " + tablename + " values ( ";
        for(int i=0;i<values.Length;i++)
        {
            string val = values[i];
            query += "\""+val+"\"";
            if(i!=values.Length-1)
            {
                query+=",";
            }
        }
        
        query+=");";
        return query;
    }
    public string UpdateTable(string tablename, string newvaltag, string newval, string oldvaltag, string oldval)
    {
        string query = "update table " + tablename + " set " + newvaltag +" = "+"\""+ newval+"\"" + " where " + oldvaltag +" = " +"\"" + oldval+"\"";      
        query+=";";
        return query;
    }
    public string SUpdateTable(string tablename, string newvaltag, string newval, string oldval)
    {
        string query = "update " + tablename + " set " + newvaltag +" = "+"\""+ newval+"\"" + " where username  = " +"\"" + oldval+"\"";   
        query+=";";
        return query;
    }
    public string SelectTable(string tablename, string username)
    {
        string query = "select * from " + tablename +" where username  = " +"\"" +username+"\"";   
        query+=";";
        return query;
    }
}
