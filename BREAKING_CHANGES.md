# 🚨 Breaking Changes

## Versão: 20260618.1051.35

⚠️ Atenção: esta atualização traz alterações importantes que podem impactar projetos que utilizam este pacote.

[Unimake.Primitives 20260618.1051.35 no NuGet](https://www.nuget.org/packages/Unimake.Primitives/20260618.1051.35)

Refatoração das estruturas de credenciais e adição de testes unitários.

- Refatorado UsernameAndPassword para UserCredentials com propriedades que lidam com valores nulos. 
- Introduzido as estruturas ClientCredentials e RefreshTokenCredentials, incluindo conversões implícitas para tokens de atualização. 
- Adicionado testes unitários para todas as estruturas de credenciais. 

### [BREAKING CHANGES] 
- Removido o tipo `UsernameAndPassword` obsoleto e atualizamos os namespaces e usings conforme necessário. Use `UserCredentials`

## Versão 20250429.140.43

[Unimake.Utils 20250429.140.43 no NuGet](https://www.nuget.org/packages/Unimake.Utils/20250429.140.43)

⚠️ Atenção: esta atualização traz alterações importantes que podem impactar projetos que utilizam este pacote.

## ✏️ Principais alterações

- Atualização no método `CNPJ.Format` para suportar o novo formato de CNPJ alfanumérico, conforme exigência da Receita Federal a partir de julho de 2026.  
    ➔ Saiba mais: [CNPJ Alfanumérico - Receita Federal](https://www.gov.br/receitafederal/pt-br/acesso-a-informacao/acoes-e-programas/programas-e-atividades/cnpj-alfanumerico)
    

## 📚 Mais informações

Em caso de dúvidas ou problemas, consulte: [https://unimake.app/problems](https://unimake.app/problems).

Agradecemos pela confiança e parceria! 🚀