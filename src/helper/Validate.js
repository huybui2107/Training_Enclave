import { emailRegex, passwordRegex } from "../constant/regex";

export const validateEmail = (text) => {
  return emailRegex.test(text);
};
export const validatePassword = (text) => {
  return passwordRegex.test(text);
};
