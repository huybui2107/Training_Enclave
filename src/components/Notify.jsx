import { Alert, Snackbar } from "@mui/material";

// eslint-disable-next-line react/prop-types
function Notify({ open, message, severity, onclose }) {
  return (
    <Snackbar
      open={open}
      autoHideDuration={2000}
      anchorOrigin={{ vertical: "bottom", horizontal: "right" }}
      onClose={onclose}
    >
      <Alert severity={severity} variant="filled" sx={{ width: "100%" }}>
        {message}
      </Alert>
    </Snackbar>
  );
}

export default Notify;
