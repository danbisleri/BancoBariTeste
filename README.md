# BancoBariTeste

Simple test of sending and receiving messages using RabbitMq in a container.
<br>
Execute Docker command:
<br>
docker run -it --rm --name rabbitmq --hostname rabbit-local -p 5672:5672 -p 15672:15672 rabbitmq:3-management
<br>
Run application in IIS.
<br>
It is possible to send messages via API.

https://localhost:44353/api/sendmessage 

With body RAW json:

{
    "Host": "5f9710bd3e63",
    "Description": "Send By API"
}
