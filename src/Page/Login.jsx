import { useState, useEffect } from "react";
import Button from "@mui/material/Button";
import CssBaseline from "@mui/material/CssBaseline";
import TextField from "@mui/material/TextField";
import FormControlLabel from "@mui/material/FormControlLabel";
import Checkbox from "@mui/material/Checkbox";

import Box from "@mui/material/Box";
import HighlightOffIcon from "@mui/icons-material/HighlightOff";
import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";
import { createTheme, ThemeProvider } from "@mui/material/styles";
import { signin } from "../api/userApi";
import { emailRegex, passwordRegex } from "../constant/regex";
import { useNavigate } from "react-router-dom";
// TODO remove, this demo shouldn't need to reset the theme.

const defaultTheme = createTheme();

export default function Login() {
  const navigate = useNavigate();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [check, setCheck] = useState(true);
  const [messageEmail, setMessageEmail] = useState("");
  const [messagePassword, setMessagePassword] = useState("");
  const [checkError, setCheckError] = useState(false);
  const validateEmail = (text) => {
    return emailRegex.test(text);
  };
  const validatePassword = (text) => {
    return passwordRegex.test(text);
  };
  const handleSubmit = async () => {
    const checkEmail = validateEmail(email);
    if (!checkEmail) setMessageEmail("The email is not formatted correctly");
    else setMessageEmail("");
    const checkPass = validatePassword(password);
    if (!checkPass)
      setMessagePassword(
        "Password must have at least 8 characters and 1 uppercase letter and 1 special character."
      );
    else setMessagePassword("");

    if (checkEmail && checkPass) {
      try {
        const res = await signin(email, password);
        localStorage.setItem("user", JSON.stringify(res.token));
        navigate(`/user?username=${email}`);
      } catch (error) {
        setCheckError(true);
      }
    }
  };
  useEffect(() => {
    if (email.length > 0 && password.length > 0) setCheck(false);
    else setCheck(true);
  }, [email, password]);

  return (
    <ThemeProvider theme={defaultTheme}>
      <Container component="main" maxWidth="xs">
        <CssBaseline />
        <Box
          sx={{
            marginTop: 8,
            display: "flex",
            flexDirection: "column",
            alignItems: "center",
          }}
        >
          {checkError && (
            <Box
              sx={{
                backgroundColor: "#ffccc7",
                width: "100%",
                height: 50,
                border: "1px solid #f5222d",
                padding: "2px",
                display: "flex",
                gap: 3,
                justifyContent: "center",
                alignItems: "center",
              }}
            >
              <HighlightOffIcon color="error" />
              <Typography>The Username or Password is incorrect</Typography>
            </Box>
          )}
          <Box sx={{ mt: 1, width: 400 }}>
            <Box>
              <TextField
                margin="normal"
                required
                fullWidth
                id="email"
                label="Email Address"
                name="email"
                autoComplete="email"
                autoFocus
                onChange={(e) => setEmail(e.target.value)}
                error={messageEmail.length > 0}
                // sx={{ borderColor: messageEmail.length > 0 ? "yellow" : "red" }}/
              />
              <Typography
                variant="subtitle2"
                gutterBottom
                sx={{ textAlign: "left", color: "red" }}
              >
                {messageEmail}
              </Typography>
            </Box>
            <Box>
              <TextField
                margin="normal"
                required
                fullWidth
                name="password"
                label="Password"
                type="password"
                id="password"
                autoComplete="current-password"
                onChange={(e) => setPassword(e.target.value)}
                error={messagePassword.length > 0}
              />
              <Typography
                variant="subtitle2"
                gutterBottom
                sx={{ textAlign: "left", color: "red" }}
              >
                {messagePassword}
              </Typography>
            </Box>
            <FormControlLabel
              control={<Checkbox value="remember" color="primary" />}
              label="Remember me"
            />
            <Button
              disabled={check}
              type="submit"
              fullWidth
              variant="contained"
              sx={{ mt: 3, mb: 2 }}
              onClick={() => handleSubmit()}
            >
              Sign In
            </Button>
          </Box>
        </Box>
      </Container>
    </ThemeProvider>
  );
}
