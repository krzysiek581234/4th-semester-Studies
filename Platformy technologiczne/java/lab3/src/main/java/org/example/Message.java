package org.example;

import java.io.Serializable;
public class Message implements Serializable
{
    private int number;
    private String content;

    public Message(String message, int number)
    {
        this.content = message;
        this.number = number;
    }
    public String getMess()
    {
        return this.content;
    }


}
