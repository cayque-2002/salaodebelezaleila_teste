const USER_KEY = "user";

export function setUser(user: any) {
  localStorage.setItem(USER_KEY, JSON.stringify(user));
}

export const getUser = () => {
  const user = localStorage.getItem("user");
  return user ? JSON.parse(user) : null;
};

export const isAdmin = () => getUser()?.tipo === 1;
export const isComum = () => getUser()?.tipo === 0;

export function clearUser() {
  localStorage.removeItem(USER_KEY);
}