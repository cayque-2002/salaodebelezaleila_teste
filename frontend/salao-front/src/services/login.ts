import { api } from "./api";

export async function login(email: string, senha: string) {
  const response = await api.post("/api/Usuario/login", {
    email,
    senha,
  });

  return response.data;
}
