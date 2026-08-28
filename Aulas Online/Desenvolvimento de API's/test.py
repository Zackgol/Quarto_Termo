import requests 
resposta = requests.get("https://api.github.com") 
print("Status da requisição:", resposta.status_code)
print("Tipo de conteúdo:", resposta.headers["Content-Type"])