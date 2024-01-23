# Push Notification API

A **Push Notification API** resolve problemas relacionados ao envio eficiente de notificações push em aplicativos web.

## Principais Funcionalidades

1. **Gerenciamento de Tokens:**
   - Utiliza a biblioteca WebPush para facilitar o registro e controle de tokens, simplificando a gestão de dispositivos para os quais as notificações push serão enviadas.

2. **Persistência de Dados:**
   - Integra o Entity Framework com PostgreSQL para garantir uma persistência robusta e escalável dos dados relacionados às notificações push.

3. **Controle de Rotas da API:**
   - Contém dois controladores principais para a API de notificações push: um para envio de notificações (PUSH) e outro para gerenciamento de usuários (USUÁRIOS).

4. **Autenticação VAPID:**
   - Implementa a autenticação VAPID (Voluntary Application Server Identification) para assegurar a segurança e a integridade das comunicações, estabelecendo a identidade do servidor de aplicativos de forma voluntária.

5. **Configuração Flexível:**
   - Permite uma configuração flexível por meio do uso de variáveis de ambiente. Pode-se personalizar a string de conexão conforme necessário, oferecendo adaptabilidade ao ambiente.

## Como Utilizar

Por padrão, a aplicação utilizará a string de conexão configurada. Você pode adicionar uma variável de ambiente para substituir a string de conexão:

```plaintext
ASPNETCORE_ENVIRONMENT_CONNECTSTRING=
