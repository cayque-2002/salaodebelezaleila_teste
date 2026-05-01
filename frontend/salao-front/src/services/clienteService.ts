import { api } from "./api";

export const listarClientes = async () => {
  const res = await api.get("/api/Cliente");
  return res.data;
};

export const criarCliente = async (data: any) => {
  const res = await api.post("/api/Cliente", data);
  return res.data;
};

export const atualizarCliente = async (id: number, data: any) => {
  const res = await api.put(`/api/Cliente/${id}`, data);
  return res.data;
};

export const deletarCliente = async (id: number) => {
  const res = await api.delete(`/api/Cliente/${id}`);
  return res.data;
};