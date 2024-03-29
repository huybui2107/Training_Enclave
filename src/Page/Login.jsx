import { useState, useEffect } from "react";
import Avatar from "@mui/material/Avatar";
import Button from "@mui/material/Button";
import CssBaseline from "@mui/material/CssBaseline";
import TextField from "@mui/material/TextField";
import FormControlLabel from "@mui/material/FormControlLabel";
import Checkbox from "@mui/material/Checkbox";

import Box from "@mui/material/Box";
import LockOutlinedIcon from "@mui/icons-material/LockOutlined";
import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";
import { createTheme, ThemeProvider } from "@mui/material/styles";

// TODO remove, this demo shouldn't need to reset the theme.

const defaultTheme = createTheme();

export default function Login() {
  const emailRegex = /^[\w\.-]+@[a-zA-Z\d\.-]+\.[a-zA-Z]{2,}$/;
  const passwordRegex =
    /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#$%^&+=])(?!.*\s).{8,}$/;
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [check, setCheck] = useState(true);
  const [messageEmail, setMessageEmail] = useState("");
  const [messagePassword, setMessagePassword] = useState("");

  const validateEmail = (text) => {
    return emailRegex.test(text);
  };
  const validatePassword = (text) => {
    return passwordRegex.test(text);
  };
  const handleSubmit = () => {
    const checkEmail = validateEmail(email);
    if (!checkEmail) setMessageEmail("The email is not formatted correctly");
    else setMessageEmail("");
    const checkPass = validatePassword(password);
    if (!checkPass) setMessagePassword("The password is not correct");
    else setMessagePassword("");
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
          <Avatar sx={{ m: 1, bgcolor: "secondary.main" }}>
            <LockOutlinedIcon />
          </Avatar>
          <Typography component="h1" variant="h5">
            Sign in
          </Typography>
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
